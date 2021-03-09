using System;
using System.Collections.Generic;
using System.Text;

namespace brunopizza
{
    class DistribuicaoDoValorAdicional
    {
        public double Pessoal { get; set; }
        public double ImportoTaxaContribuicoes { get; set; }
        public double RemuneracaoCapitalTerceiro { get; set; }
        public double RemuneracaoCapitalProprio { get; set; }

        public DistribuicaoDoValorAdicional()
        {
        }

        public DistribuicaoDoValorAdicional(double pessoal, double importoTaxaContribuicoes, double remuneracaoCapitalTerceiro, double remuneracaoCapitalProprio)
        {
            Pessoal = pessoal;
            ImportoTaxaContribuicoes = importoTaxaContribuicoes;
            RemuneracaoCapitalTerceiro = remuneracaoCapitalTerceiro;
            RemuneracaoCapitalProprio = remuneracaoCapitalProprio;
        }
    }
}
