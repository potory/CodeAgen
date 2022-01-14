﻿using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Code.Basic.CodeNames;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.MethodMembers.Events
{
    public class CodeEventUnsubscribe : CodeTabbable
    {
        private readonly CodeNameVar _eventName;
        private readonly CodeRawString _methodGroup;

        public CodeEventUnsubscribe(CodeNameVar eventName, CodeRawString methodGroup)
        {
            _eventName = eventName;
            _methodGroup = methodGroup;
        }
        
        protected override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            _eventName.Build(output);
            output.Write(CodeMarkups.EventUnsubscribe);
            _methodGroup.Build(output);
            output.Write(CodeMarkups.Semicolon);
            output.NextLine();
        }
    }
}