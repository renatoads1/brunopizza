using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace brunopizza
{

    class Program
    {
        //guarda os códigos a serem trabalhados
        public static List<SaldoctbMensalTotal> saldomes = new List<SaldoctbMensalTotal>();
        //consolidacaoSaldosContaSinteticaEAnalitica = coSaCoSiEAn para os burros
        public static List<SaldoctbMensalTotal> coSaCoSiEAn = new List<SaldoctbMensalTotal>();

        public static List<MovimentoContaDVA> movicontdva = new List<MovimentoContaDVA>();
        // 1º
        public static void Main(string[] args)
        {
            Console.WriteLine("digite o código de uma empresa");
            string codemp = Console.ReadLine();
            Console.WriteLine("digite um ano no formato yyyy");
            int ano = Convert.ToInt32(Console.ReadLine());

            //instancias de classes
            Program b = new Program();

            // recebe o retorno de todos as contas da empresa numa lista 
            List<string> bit = b.BuscaCodigoContaCtb(codemp, ano);


            foreach (var item in bit)
            {

                //calcula os dados retornados e adciona na lista de objetos SaldoctbMensalTotal
                b.CalculaSaldo(item, ano, codemp);
            }

            string[] ctc = new string[] { "2600", "2602", "2771", "2798", "2826", "2827", "2828", "2829", "2830", "2831", "2832", "2833", "2834", "2835", "2857", "2884", "2890", "2891", "3000", "3003", "3008", "3030", "3040", "3048", "3049", "3050", "3051", "3052", "3053", "3060", "3061", "3063", "3088", "3097", "3098", "3120", "3127", "3128", "3130", "3131", "3132", "3133", "3135", "3138", "3141", "3142", "3145", "3147", "3148", "3168", "3193", "3200", "3201", "3203", "3204", "3205", "3206", "3208", "3211", "3213", "3214", "3215", "3217", "3218", "3220", "3221", "3241", "3465", "3492", "3523", "3548", "3572", "3573", "3607", "3654", "3701", "3708", "3709", "3711", "3712", "3713", "3714", "3716", "3719", "3721", "3722", "3723", "3725", "3726", "3727", "3728", "3729", "3749", "3774", "3781", "3782", "3784", "3785", "3786", "3787", "3789", "3792", "3794", "3795", "3796", "3798", "3799", "3800", "3801", "3802", "3822", "3846", "3873", "3904", "3929", "3953", "3954", "3988", "4013", "4020", "4021", "4023", "4024", "4025", "4026", "4028", "4031", "4033", "4034", "4035", "4037", "4038", "4039", "4040", "4041", "4061", "4083", "4112", "4137", "4139", "4160", "4183", "4212", "4217", "4239", "4240", "4248", "4281", "4304", "4327", "4334", "4335", "4337", "4338", "4339", "4340", "4342", "4345", "4347", "4348", "4349", "4351", "4352", "4353", "4354", "4355", "4375", "4404", "4429", "4431", "4452", "4475", "4504", "4509", "4531", "4532", "4580", "4604", "4608", "4632", "4656", "4657", "4658", "4659", "4660", "4661", "4662", "4663", "4665", "4666", "4667", "4668", "4669", "4670", "4671", "4672", "4673", "4674", "4696", "4699", "4700", "4701", "4702", "4734", "4758", "4800", "4828", "4829", "4871", "4907", "4908", "4909", "4910", "4911", "4918", "4919", "4920", "4974", "4986", "5007", "5014", "5015", "5016", "5017", "5018", "5019", "5033", "5034", "5035", "5036", "5037", "5038", "5055", "5056", "5057", "5060", "5066", "5067", "5069", "5073", "5097", "5098", "5099", "5100", "5101", "5102", "5103", "5105", "5108", "5109", "5110", "5111", "5112", "5142", "5148", "5150", "5154", "5160", "5161", "5164", "5176", "5188", "5220", "5225", "5227", "5229", "5231", "5233", "5235", "5240", "5254", "5375", "5379", "5390", "5405", "5407", "5419", "5423", "5464", "5467", "5472", "5475", "5476", "5483", "5485", "5486", "5487", "5488", "5490", "5491", "5497", "5520", "5534" };

            foreach (var item in ctc)
            {
                b.ConsultaTipoConta(item);
            }


            foreach (var item in movicontdva)
            {
                if (Convert.ToInt32(item.NatursaldoDva) < 0)
                {
                    item.MovContaCtbDva = Math.Round((item.MovContaCtbDvaCred - item.MovContaCtbDvaDeb), 2);
                }
                else
                {
                    item.MovContaCtbDva = Math.Round((item.MovContaCtbDvaDeb - item.MovContaCtbDvaCred), 2);
                }

            }
            //Receitas
            var za = b.Receitas();
            Console.WriteLine("1) RECEITAS (Soma dos Itens de 1.1. a 1.4) => " + za.ToString("F2"));
            //vendas de mercadorias produtos e servicos
            var l = b.VendasDeMercadoriasProdutosServicos();
            Console.WriteLine("VendasDeMercadoriasProdutosServicos => " + l.ToString("F2"));
            //outras receitas
            var m = b.OutrasReceitas();
            Console.WriteLine("OutrasReceitas => " + m.ToString("F2"));
            //ReceitasRelativasConstrucaoDeAtivosProprios
            var ab = b.ReceitasRelativasConstrucaoDeAtivosProprios();
            Console.WriteLine("ReceitasRelativasConstrucaoDeAtivosProprios => " + ab.ToString("F2"));
            //Provisao Para devedores duvidosos
            var n = b.ProvisaoParaDevedoresDuvidosos();
            Console.WriteLine("ProvisaoParaDevedoresDuvidosos => " + n.ToString("F2"));                        
            //(=) RECEITAS (Soma dos Itens de 1.1. a 1.3)
            var zb = b.Receitas();
            Console.WriteLine("(=) RECEITAS (Soma dos Itens de 1.1. a 1.3) => " + zb.ToString("F2"));

            //2) INSUMOS ADQUIRIDOS DE TERCEIROS (de 2.1 a 2.4)
            var zc = b.InsumosAdquiridosDeTerceiroseitas();
            Console.WriteLine("2) INSUMOS ADQUIRIDOS DE TERCEIROS (de 2.1 a 2.4) => " + zc.ToString("F2"));
            //CustoDosProdutosMercadoriasEServicosVendidos() converter para valor negativo o result
            var p = b.CustoDosProdutosMercadoriasEServicosVendidos();
            Console.WriteLine("CustoDosProdutosMercadoriasEServicosVendidos => " + p.ToString("F2"));
            //MateriaisEnergiaServicosTerceirosOutros() converter para valor negativo o result
            var q = b.MateriaisEnergiaServicosTerceirosOutros();
            Console.WriteLine("MateriaisEnergiaServicosTerceirosOutros => " + q.ToString("F2"));
            //PerdaRecuperacaodeValoresAtivos() 
            var ac = b.PerdaRecuperacaodeValoresAtivos();
            Console.WriteLine("PerdaRecuperacaodeValoresAtivos => " + ac.ToString("F2"));
            //OutrasNoValorAdicionado() 
            var ad = b.OutrasNoValorAdicionado();
            Console.WriteLine("Outras => " + ad.ToString("F2"));

            //3) VALOR ADICIONADO BRUTO (1-2)
            var zd = b.ValorAdcionadoBruto();
            Console.WriteLine("3) VALOR ADICIONADO BRUTO (1-2) => " + zd.ToString("F2"));

            //4) RETENÇÕES
            var ze = b.DepreciacaoAmortizacaoExaustao();
            Console.WriteLine("4) RETENÇÕES => " + ze.ToString("F2"));
            //DepreciacaoAmortizacaoExaustao() converter para valor negativo o result
            var r = b.DepreciacaoAmortizacaoExaustao();
            Console.WriteLine("DepreciacaoAmortizacaoExaustao => " + r.ToString("F2"));

            //5) VALOR ADICIONADO LÍQ. PRODUZIDO PELA ENTIDADE (3-4)
            var zf = b.ValorAdcionadoLiquidoProdEntidade();
            Console.WriteLine("5) VALOR ADICIONADO LÍQ. PRODUZIDO PELA ENTIDADE (3-4) => " + zf.ToString("F2"));

            //6) VALOR ADICIONADO RECEBIDO EM TRANSFERÊNCIA
            var zg = b.ValorAdcionadoRecebidoEmTransferencia();
            Console.WriteLine("6) VALOR ADICIONADO RECEBIDO EM TRANSFERÊNCIA => " + zg.ToString("F2"));
            //ResultadoEquivalenciaPatrimonial()
            var s = b.ResultadoEquivalenciaPatrimonial();
            Console.WriteLine("ResultadoEquivalenciaPatrimonial => " + s.ToString("F2"));
            //ReceitaFinanceiras()
            var t = b.ReceitaFinanceiras();
            Console.WriteLine("ReceitaFinanceiras => " + t.ToString("F2"));
            //Outras()
            var u = b.Outras();
            Console.WriteLine("Outras => " + u.ToString("F2"));

            //7) VALOR ADICIONADO TOTAL A DISTRIBUIR (5+6)
            var zh = b.ValorAdcionadoTotalADistribui();
            Console.WriteLine("7) VALOR ADICIONADO TOTAL A DISTRIBUIR (5+6) => " + zh.ToString("F2"));

            //8) DISTRIBUIÇÃO DO VALOR ADICIONADO
            var zi = b.DistribucaoDoValorAdcionado();
            Console.WriteLine("8) DISTRIBUIÇÃO DO VALOR ADICIONADO => " + zi.ToString("F2"));
            //RemuneracaoDireta()


            //8.1 - Pessoal
            var zj = b.Pessoal();
            Console.WriteLine("8.1 - Pessoal => " + zj.ToString("F2"));
            //RemuneracaoDireta()
            var v = b.RemuneracaoDireta();
            Console.WriteLine("RemuneracaoDireta => " + v.ToString("F2"));
            //Beneficios()
            var x = b.Beneficios();
            Console.WriteLine("Beneficios => " + x.ToString("F2"));
            //Fgts()
            var y = b.Fgts();
            Console.WriteLine("Fgts => " + y.ToString("F2"));

            //8.2 - Impostos, Taxas e Contribuições
            var zk = b.ImportoTaxaContribuicoes();
            Console.WriteLine("8.2 - Impostos, Taxas e Contribuições => " + zk.ToString("F2"));
            //Federais()
            var z = b.Federais();
            Console.WriteLine("Federais => " + z.ToString("F2"));
            //Estaduais()
            var aa = b.Estaduais();
            Console.WriteLine("Estaduais => " + aa.ToString("F2"));
            //Municipais()
            var bb = b.Municipais();
            Console.WriteLine("Municipais => " + bb.ToString("F2"));

            //8.3 - Remuneração de Capitais de Terceiros
            var zl = b.RemuneracaoCapitalTerceiro();
            Console.WriteLine("8.3 - Remuneração de Capitais de Terceiros => " + zl.ToString("F2"));
            //Juros()
            var cc = b.Juros();
            Console.WriteLine("Juros => " + cc.ToString("F2"));
            //Alugueis()
            var dd = b.Alugueis();
            Console.WriteLine("Alugueis => " + dd.ToString("F2"));
            //OutrasNaDistribuicao()
            var ee = b.OutrasNaDistribuicao();
            Console.WriteLine("OutrasNaDistribuicao => " + ee.ToString("F2"));

            //8.4 - Remuneração de Capitais Próprios
            var zm = b.RemuneracaoCapitalProprio();
            Console.WriteLine("8.4 - Remuneração de Capitais Próprios => " + zm.ToString("F2"));
            //JurosSobreCapitalProprio()
            var ff = b.JurosSobreCapitalProprio();
            Console.WriteLine("JurosSobreCapitalProprio => " + ff.ToString("F2"));
            //Dividendos()
            var ae = b.Dividendos();
            Console.WriteLine("Dividendos => " + ae.ToString("F2"));
            //LucrosRetidosPrejuizosDoExercicio()
            var gg = b.LucrosRetidosPrejuizosDoExercicio();
            Console.WriteLine("LucrosRetidosPrejuizosDoExercicio => " + gg.ToString("F2"));
            //ParticipacaoDosNaoControladoresNosLucrosRetidos()
            var af = b.ParticipacaoDosNaoControladoresNosLucrosRetidos();
            Console.WriteLine("ParticipacaoDosNaoControladoresNosLucrosRetidos => " + af.ToString("F2"));


            


            //foreach (var item in movicontdva)
            //{
            //    Console.WriteLine("CodigoPadraoDva " + item.CodigoPadraoDva.ToString());
            //    Console.WriteLine("ContactbDva " + item.ContactbDva.ToString());
            //    Console.WriteLine("TipocontaDva " + item.TipocontaDva.ToString());
            //    Console.WriteLine("ClassifcontaDva " + item.ClassifcontaDva.ToString());
            //    Console.WriteLine("DescrcontaDva " + item.DescrcontaDva.ToString());
            //    Console.WriteLine("NatursaldoDva " + item.NatursaldoDva.ToString());
            //    Console.WriteLine("MovContaCtbDva " + item.MovContaCtbDva.ToString());
            //    Console.WriteLine("MovContaCtbDvaTotal " + item.MovContaCtbDvaTotal.ToString());
            //    Console.WriteLine("MovContaCtbDvaDeb " + item.MovContaCtbDvaDeb.ToString());
            //    Console.WriteLine("MovContaCtbDvaCred " + item.MovContaCtbDvaCred.ToString());
            //    Console.WriteLine("#############################################");
            //}

        }

        //busca os codigos "1.0.1.etc" na tabela PLANOPADRAO enquanto CONTACTB = x.y.w.z
        //4º
        public void ConsultaTipoConta(string contactb)
        {
            //busca o codigo maldito 1.0.1.etc
            string querytc = $"SELECT CODIGOPADRAO, CONTACTB, TIPOCONTA, CLASSIFCONTA, DESCRCONTA, " +
                $" NATURSALDO FROM PLANOPADRAO WHERE " +
                $"(CODIGOPADRAO = 11 or CODIGOPADRAO is null ) and CONTACTB = {contactb}";

            using (FbConnection dbConntc = new FbConnection(DAO.connFb))
            {
                FbCommand myCommandtc = new FbCommand(querytc, dbConntc);
                dbConntc.Open();
                myCommandtc.CommandTimeout = 0;
                FbDataReader myReadertc = myCommandtc.ExecuteReader();

                if (myReadertc.HasRows)
                {
                    while (myReadertc.Read())
                    {
                        //verifica o tipo da conta retornada pelo banco da query acima 
                        if (myReadertc["TIPOCONTA"].ToString() == "1")
                        {
                            var clasfconta = myReadertc["CLASSIFCONTA"].ToString();
                            //busca na lista de acordo com 4.x.y.w
                            //var filtradas = saldomes.Where(c => c.Classifconta.Contains(clasfconta));
                            var filtradas = saldomes.Where(c => c.Classifconta.StartsWith(clasfconta));

                            var CodigoempresaV = "";
                            var ContactbV = "";
                            var ClassifcontaV = "";
                            var DescrcontaV = "";
                            double MovContaCtbV = 0;
                            var NatursaldoV = "";
                            var TipocontaV = "";
                            double deb = 0;
                            double cred = 0;
                            //carrega no novo objeto o retorno da lista somado o MovContaCtb
                            foreach (var item in filtradas)
                            {
                                CodigoempresaV = item.Codigoempresa;
                                ContactbV = item.Contactb;
                                ClassifcontaV = item.Classifconta;
                                DescrcontaV = item.Descrconta;
                                deb += item.Valordeb;
                                cred += item.Valorcred;
                                MovContaCtbV += item.MovContaCtb;
                                NatursaldoV = item.Natursaldo;
                                TipocontaV = item.Tipoconta;
                            }
                            //carrega novo objeto
                            movicontdva.Add(new MovimentoContaDVA
                            {
                                CodigoPadraoDva = myReadertc["CODIGOPADRAO"].ToString(),
                                ContactbDva = myReadertc["CONTACTB"].ToString(),
                                TipocontaDva = myReadertc["TIPOCONTA"].ToString(),
                                ClassifcontaDva = myReadertc["CLASSIFCONTA"].ToString(),
                                DescrcontaDva = myReadertc["DESCRCONTA"].ToString(),
                                NatursaldoDva = myReadertc["NATURSALDO"].ToString(),
                                MovContaCtbDva = MovContaCtbV,
                                MovContaCtbDvaCred = cred,
                                MovContaCtbDvaDeb = deb
                            });



                        }
                        else if (myReadertc["TIPOCONTA"].ToString() == "2")
                        {
                            //faz a mesma merda
                            var CodigoempresaV = "";
                            var ContactbV = "";
                            var ClassifcontaV = "";
                            var DescrcontaV = "";
                            double MovContaCtbV = 0;
                            var NatursaldoV = "";
                            var TipocontaV = "";
                            double deb = 0;
                            double cred = 0;

                            var clasfconta = myReadertc["CLASSIFCONTA"].ToString();

                            var filtradas = saldomes.Where(c => c.Contactb == contactb);

                            foreach (var item in filtradas)
                            {
                                CodigoempresaV = item.Codigoempresa;
                                ContactbV = item.Contactb;
                                ClassifcontaV = item.Classifconta;
                                DescrcontaV = item.Descrconta;
                                MovContaCtbV += item.MovContaCtb;
                                NatursaldoV = item.Natursaldo;
                                TipocontaV = item.Tipoconta;
                                deb = item.Valordeb;
                                cred = item.Valorcred;
                            }

                            movicontdva.Add(new MovimentoContaDVA
                            {
                                CodigoPadraoDva = myReadertc["CODIGOPADRAO"].ToString(),
                                ContactbDva = myReadertc["CONTACTB"].ToString(),
                                TipocontaDva = myReadertc["TIPOCONTA"].ToString(),
                                ClassifcontaDva = myReadertc["CLASSIFCONTA"].ToString(),
                                DescrcontaDva = myReadertc["DESCRCONTA"].ToString(),
                                NatursaldoDva = myReadertc["NATURSALDO"].ToString(),
                                MovContaCtbDva = MovContaCtbV,
                                MovContaCtbDvaCred = cred,
                                MovContaCtbDvaDeb = deb
                            });

                        }

                    }
                }
                else
                {
                    Console.WriteLine("query não retornou valor " + querytc);
                }
            }


        }

        public List<string> BuscaCodigoContaCtb(string codemp, int ano)
        {
            string querya = "select distinct(a.contactb) from SALDOCTBMENSAL  as a, PLANOESPEC AS b " +
                $" WHERE A.CODIGOEMPRESA = {codemp}  AND A.TIPOLANCAMENTO in ('LN', 'LS') " +
                $" AND A.DATASALDO between '01.01.{ano}' and '31.12.{ano}' AND A.CONTACTB = B.CONTACTB " +
                $" AND B.CODIGOEMPRESA = A.CODIGOEMPRESA  AND((B.CLASSIFCONTA like '4.%')" +
                $" or(B.CLASSIFCONTA like '5.%') or (B.CLASSIFCONTA like '6.%')) " +
                $" order by A.CONTACTB";
            List<string> contactb = new List<string>();
            using (FbConnection dbConn = new FbConnection(DAO.connFb))
            {
                FbCommand myCommand = new FbCommand(querya, dbConn);
                dbConn.Open();
                myCommand.CommandTimeout = 0;
                var myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    contactb.Add(myReader["CONTACTB"].ToString());
                }
            }
            return contactb;
        }

        public double[] CalculaZZ(string codigoctb, int ano, string codemp)
        {

            string sqlZZ = $"select A.contactb, A.datasaldo, A.valordeb, A.valorcred, B.contactb,B.classifconta " +
                $" from (select CONTACTB, DATASALDO, sum(VALORDEB) VALORDEB, sum(VALORCRED) VALORCRED " +
                $" from( select CONTACTBDEB CONTACTB, DATALCTOCTB DATASALDO, sum(VALORLCTOCTB) VALORDEB," +
                $" cast(0 as Numeric(14, 2)) VALORCRED from LCTOCTB where CODIGOEMPRESA = {codemp} " +
                $" and CONTACTBDEB = {codigoctb} and DATALCTOCTB between '01.01.{ano}' and '31.12.{ano}'  " +
                $" and DATALCTOCTB between '01.01.{ano}' and '31.12.{ano}' AND(CODIGOORIGLCTOCTB IN('ZZ')) " +
                $" and TIPOLANCAMENTO in ('LN','LS') and CONTACTBDEB is not null " +
                $" group by CONTACTBDEB, DATALCTOCTB union all select CONTACTBCRED CONTACTB, DATALCTOCTB " +
                $" DATASALDO, cast(0 as Numeric(14, 2)) VALORDEB, sum(VALORLCTOCTB) VALORCRED  " +
                $" from LCTOCTB  where CODIGOEMPRESA = {codemp} and CONTACTBCRED = {codigoctb} and DATALCTOCTB " +
                $" between '01.01.{ano}' and '31.12.{ano}' and DATALCTOCTB between '01.01.{ano}' " +
                $" and '31.12.{ano}' AND(CODIGOORIGLCTOCTB IN('ZZ'))  and TIPOLANCAMENTO " +
                $" in ('LN', 'LS') and CONTACTBCRED is not null group by CONTACTBCRED, DATALCTOCTB)" +
                $" aliasmssql group by CONTACTB, DATASALDO ) AS A, PLANOESPEC AS B " +
                $" where A.contactb = B.contactb and B.CODIGOEMPRESA = {codemp} and A.contactb = {codigoctb} " +
                $" AND((B.CLASSIFCONTA like '4.%') or(B.CLASSIFCONTA like '5.%') or(B.CLASSIFCONTA like '6.%'))" +
                $" order by VALORDEB";

            double[] lol = new double[2] { 0, 0 };
            using (FbConnection dbConnzz = new FbConnection(DAO.connFb))
            {
                FbCommand myCommand = new FbCommand(sqlZZ, dbConnzz);
                dbConnzz.Open();
                myCommand.CommandTimeout = 0;
                var myReaderzz = myCommand.ExecuteReader();

                double valordebzz = 0;
                double valorcredzz = 0;
                if (myReaderzz.HasRows)
                {
                    while (myReaderzz.Read())
                    {
                        valordebzz += Convert.ToDouble(myReaderzz["VALORDEB"].ToString());
                        valorcredzz += Convert.ToDouble(myReaderzz["VALORCRED"].ToString());
                    }
                }

                lol[0] = Math.Round(valordebzz, 2);
                lol[1] = Math.Round(valorcredzz, 2);

            }
            return lol;
        }

        public void CalculaSaldo(string codigoctb, int ano, string codemp)
        {


            string queryb = "SELECT A.CODIGOEMPRESA, A.CONTACTB, A.DATASALDO, A.VALORDEB, A.VALORCRED, " +
                $" B.CONTACTB, B.TIPOCONTA, B.CLASSIFCONTA, B.DESCRCONTA, B.NATURSALDO FROM " +
                $" SALDOCTBMENSAL AS A, PLANOESPEC AS B WHERE A.CONTACTB = {codigoctb} " +
                $" AND A.CODIGOEMPRESA = {codemp} " +
                $" AND A.TIPOLANCAMENTO in ('LN', 'LS') AND A.DATASALDO between '01.01.{ano}' and " +
                $" '31.12.{ano}' AND A.CONTACTB = B.CONTACTB AND B.CODIGOEMPRESA = A.CODIGOEMPRESA " +
                $" AND((B.CLASSIFCONTA like '4.%') or(B.CLASSIFCONTA like '5.%') or " +
                $" (B.CLASSIFCONTA like '6.%')) order by A.CONTACTB ";

            using (FbConnection dbConnb = new FbConnection(DAO.connFb))
            {
                FbCommand myCommandb = new FbCommand(queryb, dbConnb);
                dbConnb.Open();
                myCommandb.CommandTimeout = 0;
                FbDataReader myReaderb = myCommandb.ExecuteReader();

                var codigoempresa = "";
                var contactb = "";
                double valordeb = 0;
                double valorcred = 0;
                var tipoconta = "";
                var classifconta = "";
                var descrconta = "";
                var natursaldo = "";
                double movcontactb = 0;

                if (myReaderb.HasRows)
                {
                    while (myReaderb.Read())
                    {
                        codigoempresa = myReaderb["CODIGOEMPRESA"].ToString();
                        contactb = myReaderb["CONTACTB"].ToString();
                        valordeb += Math.Round(Convert.ToDouble(myReaderb["VALORDEB"].ToString()), 2);
                        valorcred += Math.Round(Convert.ToDouble(myReaderb["VALORCRED"].ToString()), 2);
                        tipoconta = myReaderb["TIPOCONTA"].ToString();
                        classifconta = myReaderb["CLASSIFCONTA"].ToString();
                        descrconta = myReaderb["DESCRCONTA"].ToString();
                        natursaldo = myReaderb["NATURSALDO"].ToString();
                    }
                }
                //cria um array de double de duas posições
                double[] ret = new double[2];
                //chama zz que retorna deb e cred nas duas posições do array
                ret = CalculaZZ(codigoctb, ano, codemp);
                //diminui o valor real do zz retornado pelo CalculaZZ
                //deb = 0 cred = 1
                valordeb = (valordeb - ret[0]);
                valorcred = (valorcred - ret[1]);

                //add to list static
                saldomes.Add(new SaldoctbMensalTotal
                {
                    Codigoempresa = codigoempresa,
                    Contactb = contactb,
                    Valordeb = valordeb,
                    Valorcred = valorcred,
                    Tipoconta = tipoconta,
                    Classifconta = classifconta,
                    Descrconta = descrconta,
                    Natursaldo = natursaldo,
                    MovContaCtb = 0
                    //movcontactb
                }); ;

            }


        }


        public double VendasDeMercadoriasProdutosServicos()
        {

            //vendas de mercadorias produtos e servicos
            double tolalzao = 0;
            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("2602"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("5007") || item.ContactbDva.Contains("5111") || item.ContactbDva.Contains("5103") || item.ContactbDva.Contains("5112"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("2771") || item.ContactbDva.Contains("2798"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }
            }
            return tolalzao;
        }

        public double OutrasReceitas()
        {
            double tolalzao = 0;
            foreach (var item in movicontdva)
            {
                //if (item.ContactbDva.Contains("5419") || item.ContactbDva.Contains("5240") || item.ContactbDva.Contains("5375") || item.ContactbDva.Contains("4734"))
                //{
                //    //tolalzao += Math.Abs(item.MovContaCtbDva);
                //    tolalzao += item.MovContaCtbDva;
                //}
                if (item.ContactbDva.Contains("5419") || item.ContactbDva.Contains("5240") )
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("4734"))
                {
                    tolalzao -= item.MovContaCtbDva;
                }

            }
            return tolalzao;
        }
        public double ReceitasRelativasConstrucaoDeAtivosProprios()
        {
            double tolalzao = 0;
            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("0000"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }

        public double ProvisaoParaDevedoresDuvidosos()
        {
            double tolalzao = 0;
            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("4304") || item.ContactbDva.Contains("5483"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }

        public double CustoDosProdutosMercadoriasEServicosVendidos()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                //item.ContactbDva.Contains("3003") || item.ContactbDva.Contains("3008") || item.ContactbDva.Contains("3030") || item.ContactbDva.Contains("5254") || item.ContactbDva.Contains("3040") || item.ContactbDva.Contains("3048") || item.ContactbDva.Contains("3049") || item.ContactbDva.Contains("4907") || item.ContactbDva.Contains("5108") || item.ContactbDva.Contains("4871") || item.ContactbDva.Contains("5472") || item.ContactbDva.Contains("3063") || item.ContactbDva.Contains("3052") || item.ContactbDva.Contains("3053") || item.ContactbDva.Contains("3088") || item.ContactbDva.Contains("5379") || item.ContactbDva.Contains("3097") || item.ContactbDva.Contains("3098") || item.ContactbDva.Contains("3050") || item.ContactbDva.Contains("3051") || item.ContactbDva.Contains("3607") || item.ContactbDva.Contains("3654") || item.ContactbDva.Contains("4911") || item.ContactbDva.Contains("5109") || item.ContactbDva.Contains("5110") || item.ContactbDva.Contains("5066") || item.ContactbDva.Contains("5067") || item.ContactbDva.Contains("3988") || item.ContactbDva.Contains("4758") || item.ContactbDva.Contains("5464")
                //if (item.ContactbDva.Contains("3003") || item.ContactbDva.Contains("3030") || item.ContactbDva.Contains("5472") || item.ContactbDva.Contains("3063") || item.ContactbDva.Contains("3088") || item.ContactbDva.Contains("3654") || item.ContactbDva.Contains("5110") || item.ContactbDva.Contains("4758") || item.ContactbDva.Contains("5464"))
                //{
                //    //tolalzao += Math.Abs(item.MovContaCtbDva);
                //    tolalzao += item.MovContaCtbDva;
                //}
                //if (item.ContactbDva.Contains("3008") || item.ContactbDva.Contains("5254") || item.ContactbDva.Contains("3040") || item.ContactbDva.Contains("3048") || item.ContactbDva.Contains("3049") || item.ContactbDva.Contains("4907") || item.ContactbDva.Contains("5108") || item.ContactbDva.Contains("4871") || item.ContactbDva.Contains("3052") || item.ContactbDva.Contains("3053") || item.ContactbDva.Contains("5379") || item.ContactbDva.Contains("3097") || item.ContactbDva.Contains("3098") || item.ContactbDva.Contains("3050") || item.ContactbDva.Contains("3051") || item.ContactbDva.Contains("3607") || item.ContactbDva.Contains("4911") || item.ContactbDva.Contains("5109") || item.ContactbDva.Contains("5066") || item.ContactbDva.Contains("5067") || item.ContactbDva.Contains("3988"))
                //{
                //    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                //    tolalzao += item.MovContaCtbDva;
                //}
                //
                //if (item.ContactbDva.Contains("5390") || item.ContactbDva.Contains("5105") || item.ContactbDva.Contains("4986") || item.ContactbDva.Contains("4909") || item.ContactbDva.Contains("5405") || item.ContactbDva.Contains("4910") || item.ContactbDva.Contains("4920") || item.ContactbDva.Contains("4908") || item.ContactbDva.Contains("5142"))
                //{
                //    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                //    tolalzao += item.MovContaCtbDva;
                //}

                if (item.ContactbDva.Contains("3003") || item.ContactbDva.Contains("3030") || item.ContactbDva.Contains("5472") 
                    || item.ContactbDva.Contains("3063") || item.ContactbDva.Contains("3088") || item.ContactbDva.Contains("3654") 
                    || item.ContactbDva.Contains("4758") || item.ContactbDva.Contains("5464"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("3008") || item.ContactbDva.Contains("5254") || item.ContactbDva.Contains("3040") 
                    || item.ContactbDva.Contains("3048") || item.ContactbDva.Contains("3049") || item.ContactbDva.Contains("4907")
                    || item.ContactbDva.Contains("5108") 
                    || item.ContactbDva.Contains("4871") || item.ContactbDva.Contains("3052") || item.ContactbDva.Contains("3053") 
                    || item.ContactbDva.Contains("5379") || item.ContactbDva.Contains("3097") || item.ContactbDva.Contains("3098") 
                    || item.ContactbDva.Contains("3050") || item.ContactbDva.Contains("3051") || item.ContactbDva.Contains("3607") 
                    || item.ContactbDva.Contains("4911") || item.ContactbDva.Contains("5109") || item.ContactbDva.Contains("5066") 
                    || item.ContactbDva.Contains("5067") || item.ContactbDva.Contains("3988"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }
                
                if (item.ContactbDva.Contains("5105") || item.ContactbDva.Contains("5405") || item.ContactbDva.Contains("5110")
                    || item.ContactbDva.Contains("5142") || item.ContactbDva.Contains("5390") || item.ContactbDva.Contains("4920")
                    || item.ContactbDva.Contains("4910") || item.ContactbDva.Contains("4909") || item.ContactbDva.Contains("4908")
                    || item.ContactbDva.Contains("4986"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }

        public double MateriaisEnergiaServicosTerceirosOutros()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("3168") || item.ContactbDva.Contains("3241") || item.ContactbDva.Contains("3465") || item.ContactbDva.Contains("3492") || item.ContactbDva.Contains("3572") || item.ContactbDva.Contains("3749") || item.ContactbDva.Contains("3822") || item.ContactbDva.Contains("3846") || item.ContactbDva.Contains("3873") || item.ContactbDva.Contains("3953") || item.ContactbDva.Contains("4061") || item.ContactbDva.Contains("4083") || item.ContactbDva.Contains("4112") || item.ContactbDva.Contains("4139") || item.ContactbDva.Contains("4183") || item.ContactbDva.Contains("4212") || item.ContactbDva.Contains("4239") || item.ContactbDva.Contains("4281") || item.ContactbDva.Contains("4375") || item.ContactbDva.Contains("4404") || item.ContactbDva.Contains("4431") || item.ContactbDva.Contains("4475") || item.ContactbDva.Contains("5488") || item.ContactbDva.Contains("5487") || item.ContactbDva.Contains("4504") || item.ContactbDva.Contains("4531") || item.ContactbDva.Contains("5491") || item.ContactbDva.Contains("5490") || item.ContactbDva.Contains("4580") || item.ContactbDva.Contains("4604"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("3573") || item.ContactbDva.Contains("4217") || item.ContactbDva.Contains("4240") || item.ContactbDva.Contains("4248") || item.ContactbDva.Contains("4509") || item.ContactbDva.Contains("4532") || item.ContactbDva.Contains("4608"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double PerdaRecuperacaodeValoresAtivos()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("0000"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double OutrasNoValorAdicionado()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("0000"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }

        public double DepreciacaoAmortizacaoExaustao()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("3548") || item.ContactbDva.Contains("3929") || item.ContactbDva.Contains("4160") || item.ContactbDva.Contains("4452") || item.ContactbDva.Contains("4632"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double ResultadoEquivalenciaPatrimonial()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("2890") || item.ContactbDva.Contains("2891") || item.ContactbDva.Contains("5148") || item.ContactbDva.Contains("5150") || item.ContactbDva.Contains("5161") || item.ContactbDva.Contains("5164"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double ReceitaFinanceiras()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("2857"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double Outras()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("2884") || item.ContactbDva.Contains("5375") || item.ContactbDva.Contains("5375"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("2890") || item.ContactbDva.Contains("2891") || item.ContactbDva.Contains("5148") || item.ContactbDva.Contains("5150"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double RemuneracaoDireta()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("3120") || item.ContactbDva.Contains("3193") || item.ContactbDva.Contains("3701") || item.ContactbDva.Contains("3774") || item.ContactbDva.Contains("4013") || item.ContactbDva.Contains("4327"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("3130") || item.ContactbDva.Contains("3131") || item.ContactbDva.Contains("3132") || item.ContactbDva.Contains("3133") || item.ContactbDva.Contains("3135") || item.ContactbDva.Contains("5097") || item.ContactbDva.Contains("3138") || item.ContactbDva.Contains("3141") || item.ContactbDva.Contains("5014") || item.ContactbDva.Contains("3142") || item.ContactbDva.Contains("5033") || item.ContactbDva.Contains("3145") || item.ContactbDva.Contains("3147") || item.ContactbDva.Contains("5176") || item.ContactbDva.Contains("5225") || item.ContactbDva.Contains("3203") || item.ContactbDva.Contains("3204") || item.ContactbDva.Contains("3205") || item.ContactbDva.Contains("3206") || item.ContactbDva.Contains("3208") || item.ContactbDva.Contains("5098") || item.ContactbDva.Contains("3211") || item.ContactbDva.Contains("3213") || item.ContactbDva.Contains("3214") || item.ContactbDva.Contains("5015") || item.ContactbDva.Contains("3215") || item.ContactbDva.Contains("3217") || item.ContactbDva.Contains("5034") || item.ContactbDva.Contains("3218") || item.ContactbDva.Contains("3220") || item.ContactbDva.Contains("3221") || item.ContactbDva.Contains("5188") || item.ContactbDva.Contains("5227") || item.ContactbDva.Contains("3711") || item.ContactbDva.Contains("3712") || item.ContactbDva.Contains("3713") || item.ContactbDva.Contains("3714") || item.ContactbDva.Contains("3716") || item.ContactbDva.Contains("5099") || item.ContactbDva.Contains("3719") || item.ContactbDva.Contains("3721") || item.ContactbDva.Contains("3722") || item.ContactbDva.Contains("5016") || item.ContactbDva.Contains("3723") || item.ContactbDva.Contains("3725") || item.ContactbDva.Contains("5035") || item.ContactbDva.Contains("3726") || item.ContactbDva.Contains("3727") || item.ContactbDva.Contains("3728") || item.ContactbDva.Contains("5229") || item.ContactbDva.Contains("3784") || item.ContactbDva.Contains("3785") || item.ContactbDva.Contains("3786") || item.ContactbDva.Contains("3787") || item.ContactbDva.Contains("3789") || item.ContactbDva.Contains("5100") || item.ContactbDva.Contains("3792") || item.ContactbDva.Contains("3794") || item.ContactbDva.Contains("3795") || item.ContactbDva.Contains("5017") || item.ContactbDva.Contains("3796") || item.ContactbDva.Contains("3798") || item.ContactbDva.Contains("5036") || item.ContactbDva.Contains("3799") || item.ContactbDva.Contains("3800") || item.ContactbDva.Contains("3801") || item.ContactbDva.Contains("3802") || item.ContactbDva.Contains("5231") || item.ContactbDva.Contains("4023") || item.ContactbDva.Contains("4024") || item.ContactbDva.Contains("4025") || item.ContactbDva.Contains("4026") || item.ContactbDva.Contains("5102") || item.ContactbDva.Contains("4028") || item.ContactbDva.Contains("5101") || item.ContactbDva.Contains("4031") || item.ContactbDva.Contains("4033") || item.ContactbDva.Contains("4034") || item.ContactbDva.Contains("5018") || item.ContactbDva.Contains("4035") || item.ContactbDva.Contains("4037") || item.ContactbDva.Contains("5037") || item.ContactbDva.Contains("4038") || item.ContactbDva.Contains("4039") || item.ContactbDva.Contains("4038") || item.ContactbDva.Contains("4040") || item.ContactbDva.Contains("4041") || item.ContactbDva.Contains("5233") || item.ContactbDva.Contains("4337") || item.ContactbDva.Contains("4338") || item.ContactbDva.Contains("4339") || item.ContactbDva.Contains("4340") || item.ContactbDva.Contains("4342") || item.ContactbDva.Contains("4974") || item.ContactbDva.Contains("4345") || item.ContactbDva.Contains("4347") || item.ContactbDva.Contains("4348") || item.ContactbDva.Contains("5019") || item.ContactbDva.Contains("4349") || item.ContactbDva.Contains("4351") || item.ContactbDva.Contains("5038") || item.ContactbDva.Contains("4352") || item.ContactbDva.Contains("4353") || item.ContactbDva.Contains("4354") || item.ContactbDva.Contains("4355") || item.ContactbDva.Contains("5220") || item.ContactbDva.Contains("5235"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("3128") || item.ContactbDva.Contains("3201") || item.ContactbDva.Contains("3709") || item.ContactbDva.Contains("3782") || item.ContactbDva.Contains("4021") || item.ContactbDva.Contains("4335"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("3127") || item.ContactbDva.Contains("3148") || item.ContactbDva.Contains("3200") || item.ContactbDva.Contains("3221") || item.ContactbDva.Contains("3708") || item.ContactbDva.Contains("3729") || item.ContactbDva.Contains("3781") || item.ContactbDva.Contains("3802") || item.ContactbDva.Contains("4020") || item.ContactbDva.Contains("4041") || item.ContactbDva.Contains("4334") || item.ContactbDva.Contains("4355"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double Beneficios()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("3130") || item.ContactbDva.Contains("3131") || item.ContactbDva.Contains("3132") || item.ContactbDva.Contains("3133") || item.ContactbDva.Contains("3135") || item.ContactbDva.Contains("5097") || item.ContactbDva.Contains("3138") || item.ContactbDva.Contains("3141") || item.ContactbDva.Contains("5014") || item.ContactbDva.Contains("3142") || item.ContactbDva.Contains("5033") || item.ContactbDva.Contains("3145") || item.ContactbDva.Contains("3147") || item.ContactbDva.Contains("5176") || item.ContactbDva.Contains("5225") || item.ContactbDva.Contains("3203") || item.ContactbDva.Contains("3204") || item.ContactbDva.Contains("3205") || item.ContactbDva.Contains("3206") || item.ContactbDva.Contains("3208") || item.ContactbDva.Contains("5098") || item.ContactbDva.Contains("3211") || item.ContactbDva.Contains("3213") || item.ContactbDva.Contains("3214") || item.ContactbDva.Contains("5015") || item.ContactbDva.Contains("3215") || item.ContactbDva.Contains("3217") || item.ContactbDva.Contains("5034") || item.ContactbDva.Contains("3218") || item.ContactbDva.Contains("3220") || item.ContactbDva.Contains("3221") || item.ContactbDva.Contains("5188") || item.ContactbDva.Contains("5227") || item.ContactbDva.Contains("3711") || item.ContactbDva.Contains("3712") || item.ContactbDva.Contains("3713") || item.ContactbDva.Contains("3714") || item.ContactbDva.Contains("3716") || item.ContactbDva.Contains("5099") || item.ContactbDva.Contains("3719") || item.ContactbDva.Contains("3721") || item.ContactbDva.Contains("3722") || item.ContactbDva.Contains("5016") || item.ContactbDva.Contains("3723") || item.ContactbDva.Contains("3725") || item.ContactbDva.Contains("5035") || item.ContactbDva.Contains("3726") || item.ContactbDva.Contains("3727") || item.ContactbDva.Contains("3728") || item.ContactbDva.Contains("5229") || item.ContactbDva.Contains("3784") || item.ContactbDva.Contains("3785") || item.ContactbDva.Contains("3786") || item.ContactbDva.Contains("3787") || item.ContactbDva.Contains("3789") || item.ContactbDva.Contains("5100") || item.ContactbDva.Contains("3792") || item.ContactbDva.Contains("3794") || item.ContactbDva.Contains("3795") || item.ContactbDva.Contains("5017") || item.ContactbDva.Contains("3796") || item.ContactbDva.Contains("3798") || item.ContactbDva.Contains("5036") || item.ContactbDva.Contains("3799") || item.ContactbDva.Contains("3800") || item.ContactbDva.Contains("3801") || item.ContactbDva.Contains("3802") || item.ContactbDva.Contains("5231") || item.ContactbDva.Contains("4023") || item.ContactbDva.Contains("4024") || item.ContactbDva.Contains("4025") || item.ContactbDva.Contains("4026") || item.ContactbDva.Contains("5102") || item.ContactbDva.Contains("4028") || item.ContactbDva.Contains("5101") || item.ContactbDva.Contains("4031") || item.ContactbDva.Contains("4033") || item.ContactbDva.Contains("4034") || item.ContactbDva.Contains("5018") || item.ContactbDva.Contains("4035") || item.ContactbDva.Contains("4037") || item.ContactbDva.Contains("5037") || item.ContactbDva.Contains("4038") || item.ContactbDva.Contains("4039") || item.ContactbDva.Contains("4038") || item.ContactbDva.Contains("4040") || item.ContactbDva.Contains("4041") || item.ContactbDva.Contains("5233") || item.ContactbDva.Contains("4337") || item.ContactbDva.Contains("4338") || item.ContactbDva.Contains("4339") || item.ContactbDva.Contains("4340") || item.ContactbDva.Contains("4342") || item.ContactbDva.Contains("4974") || item.ContactbDva.Contains("4345") || item.ContactbDva.Contains("4347") || item.ContactbDva.Contains("4348") || item.ContactbDva.Contains("5019") || item.ContactbDva.Contains("4349") || item.ContactbDva.Contains("4351") || item.ContactbDva.Contains("5038") || item.ContactbDva.Contains("4352") || item.ContactbDva.Contains("4353") || item.ContactbDva.Contains("4354") || item.ContactbDva.Contains("4355") || item.ContactbDva.Contains("5220") || item.ContactbDva.Contains("5235"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double Fgts()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("3128") || item.ContactbDva.Contains("3201") || item.ContactbDva.Contains("3709") || item.ContactbDva.Contains("3782") || item.ContactbDva.Contains("4021") || item.ContactbDva.Contains("4335"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }


            }
            return tolalzao;

        }
        public double Federais()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("5007") || item.ContactbDva.Contains("5111") || item.ContactbDva.Contains("5103") || item.ContactbDva.Contains("5112") || item.ContactbDva.Contains("2826") || item.ContactbDva.Contains("2829") || item.ContactbDva.Contains("2830") || item.ContactbDva.Contains("2831") || item.ContactbDva.Contains("2833") || item.ContactbDva.Contains("3060") || item.ContactbDva.Contains("3061") || item.ContactbDva.Contains("5060") || item.ContactbDva.Contains("4986") || item.ContactbDva.Contains("4920") || item.ContactbDva.Contains("3127") || item.ContactbDva.Contains("3148") || item.ContactbDva.Contains("3200") || item.ContactbDva.Contains("3221") || item.ContactbDva.Contains("5109") || item.ContactbDva.Contains("5110") || item.ContactbDva.Contains("3708") || item.ContactbDva.Contains("3729") || item.ContactbDva.Contains("3781") || item.ContactbDva.Contains("3802") || item.ContactbDva.Contains("4020") || item.ContactbDva.Contains("4041") || item.ContactbDva.Contains("4334") || item.ContactbDva.Contains("4355") || item.ContactbDva.Contains("4656") || item.ContactbDva.Contains("5520") || item.ContactbDva.Contains("4659") || item.ContactbDva.Contains("4662") || item.ContactbDva.Contains("4660") || item.ContactbDva.Contains("4661") || item.ContactbDva.Contains("4666") || item.ContactbDva.Contains("5057") || item.ContactbDva.Contains("4667") || item.ContactbDva.Contains("4670") || item.ContactbDva.Contains("4671") || item.ContactbDva.Contains("4672") || item.ContactbDva.Contains("4828") || item.ContactbDva.Contains("4829"))
                {
                    tolalzao += (item.MovContaCtbDva);
                }
                if (item.ContactbDva.Contains("2834") || item.ContactbDva.Contains("3048") || item.ContactbDva.Contains("3049") || item.ContactbDva.Contains("5108") || item.ContactbDva.Contains("3052") || item.ContactbDva.Contains("3053") || item.ContactbDva.Contains("3098") || item.ContactbDva.Contains("3050") || item.ContactbDva.Contains("3051") || item.ContactbDva.Contains("5066") || item.ContactbDva.Contains("5067") || item.ContactbDva.Contains("5485") || item.ContactbDva.Contains("5486") || item.ContactbDva.Contains("5488") || item.ContactbDva.Contains("5487") || item.ContactbDva.Contains("5491") || item.ContactbDva.Contains("5490") || item.ContactbDva.Contains("5476") || item.ContactbDva.Contains("5475"))
                {
                    tolalzao -= (item.MovContaCtbDva);
                }
                if (item.ContactbDva.Contains("5423"))
                {
                    tolalzao -= (item.MovContaCtbDva);
                }

            }
            return tolalzao;

        }
        public double Estaduais()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("2827") || item.ContactbDva.Contains("2832") || item.ContactbDva.Contains("5467") || item.ContactbDva.Contains("5390") || item.ContactbDva.Contains("5105") || item.ContactbDva.Contains("4909") || item.ContactbDva.Contains("5405") || item.ContactbDva.Contains("4910") || item.ContactbDva.Contains("4908") || item.ContactbDva.Contains("5142") || item.ContactbDva.Contains("4911") || item.ContactbDva.Contains("4657") || item.ContactbDva.Contains("4669") || item.ContactbDva.Contains("4673") || item.ContactbDva.Contains("5056") || item.ContactbDva.Contains("4918") || item.ContactbDva.Contains("4919") || item.ContactbDva.Contains("5055"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("3008") || item.ContactbDva.Contains("5254") || item.ContactbDva.Contains("3040") || item.ContactbDva.Contains("4907") || item.ContactbDva.Contains("4871") || item.ContactbDva.Contains("5379") || item.ContactbDva.Contains("3097") || item.ContactbDva.Contains("2835"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("5534") || item.ContactbDva.Contains("5154"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double Municipais()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("2828") || item.ContactbDva.Contains("3573") || item.ContactbDva.Contains("3954") || item.ContactbDva.Contains("4240") || item.ContactbDva.Contains("4532") || item.ContactbDva.Contains("4658") || item.ContactbDva.Contains("4663") || item.ContactbDva.Contains("4665") || item.ContactbDva.Contains("4668") || item.ContactbDva.Contains("4674"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double Juros()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("4699") || item.ContactbDva.Contains("5160") || item.ContactbDva.Contains("5497") || item.ContactbDva.Contains("4700") || item.ContactbDva.Contains("4701") || item.ContactbDva.Contains("5407"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }

        public double Alugueis()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("3523") || item.ContactbDva.Contains("5069") || item.ContactbDva.Contains("5073") || item.ContactbDva.Contains("3904") || item.ContactbDva.Contains("4137") || item.ContactbDva.Contains("4217") || item.ContactbDva.Contains("4248") || item.ContactbDva.Contains("4429") || item.ContactbDva.Contains("5485") || item.ContactbDva.Contains("5486") || item.ContactbDva.Contains("4509") || item.ContactbDva.Contains("4608"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("4139") || item.ContactbDva.Contains("4431"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double OutrasNaDistribuicao()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("4696"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("4699") || item.ContactbDva.Contains("5160") || item.ContactbDva.Contains("5497") || item.ContactbDva.Contains("4700") || item.ContactbDva.Contains("4701") || item.ContactbDva.Contains("4702") || item.ContactbDva.Contains("5161") || item.ContactbDva.Contains("5164") || item.ContactbDva.Contains("5407") || item.ContactbDva.Contains("5483"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double JurosSobreCapitalProprio()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("4702"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double Dividendos()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("0000"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double LucrosRetidosPrejuizosDoExercicio()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("2600"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }
                if (item.ContactbDva.Contains("3000") || item.ContactbDva.Contains("4800"))
                {
                    //tolalzao -= Math.Abs(item.MovContaCtbDva);
                    tolalzao -= item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }
        public double ParticipacaoDosNaoControladoresNosLucrosRetidos()
        {
            double tolalzao = 0;

            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("0000"))
                {
                    //tolalzao += Math.Abs(item.MovContaCtbDva);
                    tolalzao += item.MovContaCtbDva;
                }

            }
            return tolalzao;

        }

        public double Receitas()
        {
            double totalzao = VendasDeMercadoriasProdutosServicos() + OutrasReceitas() + ReceitasRelativasConstrucaoDeAtivosProprios() + ProvisaoParaDevedoresDuvidosos();
            return totalzao;
        
        
        }
        public double InsumosAdquiridosDeTerceiroseitas()
        {
            double totalzao = CustoDosProdutosMercadoriasEServicosVendidos() + MateriaisEnergiaServicosTerceirosOutros() + PerdaRecuperacaodeValoresAtivos() + OutrasNoValorAdicionado();
            return totalzao;


        }

        public double ValorAdcionadoBruto()
        {
            double totalzao = Receitas() + InsumosAdquiridosDeTerceiroseitas();
            return totalzao;
        }

        public double ValorAdcionadoLiquidoProdEntidade()
        {
            double totalzao = ValorAdcionadoBruto() + DepreciacaoAmortizacaoExaustao();
            return totalzao;
        }
        public double ValorAdcionadoRecebidoEmTransferencia()
        {
            double totalzao = ResultadoEquivalenciaPatrimonial() + ReceitaFinanceiras() + Outras();
            return totalzao;
        }


        public double ValorAdcionadoTotalADistribui()
        {
            double totalzao = ValorAdcionadoLiquidoProdEntidade() + ValorAdcionadoRecebidoEmTransferencia();
            return totalzao;
        }
        public double Pessoal()
        {
            double totalzao = RemuneracaoDireta() + Beneficios() + Fgts();
            return totalzao;
        }
        public double ImportoTaxaContribuicoes()
        {
            double totalzao = Federais() + Estaduais() + Municipais();
            return totalzao;
        }
        public double RemuneracaoCapitalTerceiro()
        {
            double totalzao = Juros() + Alugueis() + OutrasNaDistribuicao();
            return totalzao;
        }
        public double RemuneracaoCapitalProprio()
        {
            double totalzao = JurosSobreCapitalProprio() + Dividendos() + LucrosRetidosPrejuizosDoExercicio() + ParticipacaoDosNaoControladoresNosLucrosRetidos();
            return totalzao;
        }
        public double DistribucaoDoValorAdcionado()
        {
            double totalzao = Pessoal() + ImportoTaxaContribuicoes() + RemuneracaoCapitalTerceiro() + RemuneracaoCapitalProprio();
            return totalzao;
        }



    }
}