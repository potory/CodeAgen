﻿using System.Collections.Generic;
using CodeAgen.Code.Basic;
using CodeAgen.Code.CodeTemplates.Extensions;
using CodeAgen.Code.CodeTemplates.Interfaces;
using CodeAgen.Code.CodeTemplates.Interfaces.Class;
using CodeAgen.Exceptions;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.ClassMembers
{
    public class CodeClassMethod : CodeBracedBlock, IAbstractable, IGenericable, ICodeClassMember
    {
        public byte Order => 255;
        
        private readonly CodeName _name;
        private CodeType _returnType = CodeType.Void;
        private CodeAccessModifier _accessModifier = CodeAccessModifier.Private;
        private List<CodeClassMethodParameter> _parameters;
        private CodeClassMethodParameter _params;
        
        // Properties
        
        public bool IsAbstract { get; set; }
        public bool HasParameters => _parameters != null && _parameters.Count > 0;
        public bool HasParams => _params != null;
        public List<CodeName> GenericArguments { get; set; }
        public List<string> GenericRestrictions { get; set; }

        // Methods
        
        public CodeClassMethod(CodeName name)
        {
            _name = name;
        }

        public CodeClassMethod SetReturnType(CodeType type)
        {
            _returnType = type;
            return this;
        }

        public CodeClassMethod SetAccess(CodeAccessModifier accessModifier)
        {
            _accessModifier = accessModifier;
            return this;
        }

        public CodeClassMethod AddParameter(CodeClassMethodParameter parameter)
        {
            if (_parameters == null)
            {
                _parameters = new List<CodeClassMethodParameter>();
            }
            
            _parameters.Add(parameter);

            return this;
        }

        public CodeClassMethod AddParams(CodeClassMethodParameter @params)
        {
            _params = @params;
            return this;
        }
        
        
        public override void Build(ICodeOutput output)
        {
            output.SetTab(Level);

            WriteHeader(output);

            output.NextLine();
            
            base.Build(output);
        }
        
        private void WriteHeader(ICodeOutput output)
        {
            output.Write(_accessModifier);
            output.Write(CodeMarkups.Space);
            
            if (IsAbstract)
            {
                this.WriteAbstract(output);
            }
            
            output.Write(_returnType);
            output.Write(CodeMarkups.Space);
            
            _name.Build(output);

            if (this.IsGeneric())
            {
                this.WriteGeneric(output);
            }
            
            output.Write(CodeMarkups.OpenBracket);

            if (HasParameters)
            {
                _parameters[0].Build(output);
                
                for (int index = 1; index < _parameters.Count; index++)
                {
                    output.Write(CodeMarkups.Comma);
                    output.Write(CodeMarkups.Space);
                    _parameters[index].Build(output);
                }
            }

            if (HasParams)
            {
                if (HasParameters)
                {
                    output.Write(CodeMarkups.Comma);
                    output.Write(CodeMarkups.Space);
                }

                output.Write(CodeKeywords.Params);
                output.Write(CodeMarkups.Space);
                _params.Build(output);
            }
            
            output.Write(CodeMarkups.CloseBracket);

            if (this.HasRestrictions())
            {
                this.WriteRestrictions(output);
            }
        }
    }

    public class CodeClassMethodParameter : CodeRaw
    {
        private readonly string _name;
        private readonly CodeType _type;
        private readonly string _defaultValue;
        public bool HasDefaultValue => _defaultValue != null;
        
        public CodeClassMethodParameter(string name, CodeType type, string defaultValue = null)
        {
            _name = name;
            _type = type;
            _defaultValue = defaultValue;
        }
        
        public override void Build(ICodeOutput output)
        {
            output.Write(_type);
            output.Write(CodeMarkups.Space);
            output.Write(_name);

            if (!HasDefaultValue)
            {
                return;
            }
            
            output.Write(CodeMarkups.Assignment);
            output.Write(_defaultValue);
        }
    }
}