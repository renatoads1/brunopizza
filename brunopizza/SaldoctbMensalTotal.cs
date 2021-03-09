using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace brunopizza
{
    class SaldoctbMensalTotal
    {
        public string Codigoempresa { get; set; }
        public string Contactb { get; set; }
        public double Valordeb { get; set; }
        public double Valorcred { get; set; }
        public string Tipoconta { get; set; }
        public string Classifconta { get; set; }
        public string Descrconta { get; set; }
        public string Natursaldo { get; set; }
        public double MovContaCtb { get; set; }

        public SaldoctbMensalTotal()
        {

        }

        public SaldoctbMensalTotal(string codigoempresa, string contactb, double valordeb, double valorcred, string tipoconta, string classifconta, string descrconta, string natursaldo, double movContaCtb)
        {
            Codigoempresa = codigoempresa;
            Contactb = contactb;
            Valordeb = valordeb;
            Valorcred = valorcred;
            Tipoconta = tipoconta;
            Classifconta = classifconta;
            Descrconta = descrconta;
            Natursaldo = natursaldo;
            MovContaCtb = movContaCtb;
        }
    }
}
