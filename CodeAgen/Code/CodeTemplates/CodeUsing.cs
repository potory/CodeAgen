﻿using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates
{
    public class CodeUsing : CodeTabbable
    {
        private readonly CodeNameNamespace _name;
        
        public CodeUsing(CodeNameNamespace name) : base()
        {
            _name = name;
        }

        public override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write($"{CodeKeywords.Using}{CodeMarkups.Space}");
            _name.OnBuild(output);
            output.Write(CodeMarkups.Semicolon);
            output.NextLine();
        }
    }
}