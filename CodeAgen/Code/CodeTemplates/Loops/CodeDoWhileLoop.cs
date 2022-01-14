﻿using CodeAgen.Code.Abstract;
using CodeAgen.Code.Basic;
using CodeAgen.Outputs;

namespace CodeAgen.Code.CodeTemplates.Loops
{
    public class CodeDoWhileLoop : CodeBracedBlock
    {
        private readonly CodeUnit _condition;

        public CodeDoWhileLoop(CodeUnit condition)
        {
            _condition = condition;
        }

        public CodeDoWhileLoop(CodeRawString condition)
        {
            _condition = condition;
        }

        public override void OnBuild(ICodeOutput output)
        {
            output.SetTab(Level);
            output.Write(CodeKeywords.Do);
            output.NextLine();
            base.OnBuild(output);
            output.SetTab(Level);
            output.Write($"{CodeKeywords.While}{CodeMarkups.OpenBracket}");
            _condition.OnBuild(output);
            output.Write(CodeMarkups.CloseBracket);
            output.NextLine();
        }
    }
}