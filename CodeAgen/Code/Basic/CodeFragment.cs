﻿using System.Collections.Generic;
using CodeAgen.Code.Abstract;
using CodeAgen.Outputs;

namespace CodeAgen.Code.Basic
{
    public class CodeFragment : CodeTabbable
    {
        private readonly List<CodeUnit> _units = new List<CodeUnit>();

        public CodeFragment AddUnit(CodeUnit unit)
        {
            if (unit is CodeTabbable tabbable)
            {
                tabbable.Level = Level;
            }
            
            _units.Add(unit);

            return this;
        }
        
        public override void Build(ICodeOutput output)
        {
            output.SetTab(Level);

            foreach (var unit in _units)
            {
                unit.Build(output);
            }

            output.SetTab(Level);
        }
    }
}