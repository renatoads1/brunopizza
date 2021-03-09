using System;
using System.Collections.Generic;
using System.Text;

namespace brunopizza
{
    class MovimentoContaDVA
    {
        public string CodigoPadraoDva { get; set; }
        public string ContactbDva { get; set; }
        public string TipocontaDva { get; set; }
        public string ClassifcontaDva { get; set; }
        public string DescrcontaDva { get; set; }
        public string NatursaldoDva { get; set; }
        public double MovContaCtbDva { get; set; }
        public double MovContaCtbDvaTotal { get; set; }


        public MovimentoContaDVA()
        {
        }

        public MovimentoContaDVA(string codigoPadraoDva, string contactbDva, string tipocontaDva, string classifcontaDva, string descrcontaDva, string natursaldoDva, double movContaCtbDva, double movContaCtbDvaTotal)
        {
            CodigoPadraoDva = codigoPadraoDva;
            ContactbDva = contactbDva;
            TipocontaDva = tipocontaDva;
            ClassifcontaDva = classifcontaDva;
            DescrcontaDva = descrcontaDva;
            NatursaldoDva = natursaldoDva;
            MovContaCtbDva = movContaCtbDva;
            MovContaCtbDvaTotal = movContaCtbDvaTotal;
        }
    }
}
