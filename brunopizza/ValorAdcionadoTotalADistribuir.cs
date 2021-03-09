using System;
using System.Collections.Generic;
using System.Text;

namespace brunopizza
{
    class ValorAdcionadoTotalADistribuir
    {
        public double Receitas { get; set; }
        public double InsumosAdquiridosDeTerceiros { get; set; }
        public double ValorAdcionadoBruto { get; set; }
        public double Retencoes { get; set; }
        public double ValorAdcionadoLiquidoProdEntidade { get; set; }
        public double ValorAdcionadoRecebidoEmTransferencia { get; set; }
        public double ValorAdcionadoTotalADistribui { get; set; }

        public ValorAdcionadoTotalADistribuir()
        {
        }

        public ValorAdcionadoTotalADistribuir(double receitas, double insumosAdquiridosDeTerceiros, double valorAdcionadoBruto, double retencoes, double valorAdcionadoLiquidoProdEntidade, double valorAdcionadoRecebidoEmTransferencia, double valorAdcionadoTotalADistribui)
        {
            Receitas = receitas;
            InsumosAdquiridosDeTerceiros = insumosAdquiridosDeTerceiros;
            ValorAdcionadoBruto = valorAdcionadoBruto;
            Retencoes = retencoes;
            ValorAdcionadoLiquidoProdEntidade = valorAdcionadoLiquidoProdEntidade;
            ValorAdcionadoRecebidoEmTransferencia = valorAdcionadoRecebidoEmTransferencia;
            ValorAdcionadoTotalADistribui = valorAdcionadoTotalADistribui;
        }
    }
}
