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

        public static List<MovimentoContaDVA> movicontdva = new List<MovimentoContaDVA>();
        public static void Main(string[] args)
        {

            Console.WriteLine("digite o código de uma empresa");
            string codemp = Console.ReadLine();
            Console.WriteLine("digite um ano no formato yyyy");
            int ano = Convert.ToInt32(Console.ReadLine());

            //instancias de classes
            Program b = new Program();

            //variaveis
            List<string> bit = b.BuscaCodigoContaCtb(codemp, ano);

            ////para cada codigo executa a funcao que calcula
            foreach (var item in bit)
            {

                //calcula os dados retornados e adciona na lista de objetos SaldoctbMensalTotal
                b.CalculaSaldo(item, ano, codemp);
            }
            //para preencher o objeto MovimentoContaDVA
            string[] ctc = new string[] { "2600", "2602", "2771", "2798", "2826", "2827", "2828", "2829", "2830", "2831", "2832", "2833", "2834", "2835", "2857", "2884", "2890", "2891", "3000", "3003", "3008", "3030", "3040", "3048", "3049", "3050", "3051", "3052", "3053", "3060", "3061", "3063", "3088", "3097", "3098", "3120", "3127", "3128", "3130", "3131", "3132", "3133", "3135", "3138", "3141", "3142", "3145", "3147", "3148", "3168", "3193", "3200", "3201", "3203", "3204", "3205", "3206", "3208", "3211", "3213", "3214", "3215", "3217", "3218", "3220", "3221", "3241", "3465", "3492", "3523", "3548", "3572", "3573", "3607", "3654", "3701", "3708", "3709", "3711", "3712", "3713", "3714", "3716", "3719", "3721", "3722", "3723", "3725", "3726", "3727", "3728", "3729", "3749", "3774", "3781", "3782", "3784", "3785", "3786", "3787", "3789", "3792", "3794", "3795", "3796", "3798", "3799", "3800", "3801", "3802", "3822", "3846", "3873", "3904", "3929", "3953", "3954", "3988", "4013", "4020", "4021", "4023", "4024", "4025", "4026", "4028", "4031", "4033", "4034", "4035", "4037", "4038", "4039", "4040", "4041", "4061", "4083", "4112", "4137", "4139", "4160", "4183", "4212", "4217", "4239", "4240", "4248", "4281", "4304", "4327", "4334", "4335", "4337", "4338", "4339", "4340", "4342", "4345", "4347", "4348", "4349", "4351", "4352", "4353", "4354", "4355", "4375", "4404", "4429", "4431", "4452", "4475", "4504", "4509", "4531", "4532", "4580", "4604", "4608", "4632", "4656", "4657", "4658", "4659", "4660", "4661", "4662", "4663", "4665", "4666", "4667", "4668", "4669", "4670", "4671", "4672", "4673", "4674", "4696", "4699", "4700", "4701", "4702", "4734", "4758", "4800", "4828", "4829", "4871", "4907", "4908", "4909", "4910", "4911", "4918", "4919", "4920", "4974", "4986", "5007", "5014", "5015", "5016", "5017", "5018", "5019", "5033", "5034", "5035", "5036", "5037", "5038", "5055", "5056", "5057", "5060", "5066", "5067", "5069", "5073", "5097", "5098", "5099", "5100", "5101", "5102", "5103", "5105", "5108", "5109", "5110", "5111", "5112", "5142", "5148", "5150", "5154", "5160", "5161", "5164", "5176", "5188", "5220", "5225", "5227", "5229", "5231", "5233", "5235", "5240", "5254", "5375", "5379", "5390", "5405", "5407", "5419", "5423", "5464", "5467", "5472", "5475", "5476", "5483", "5485", "5486", "5487", "5488", "5490", "5491", "5497", "5520", "5534" };

            foreach (var item in ctc)
            {
                b.ConsultaTipoConta(item);
            }


            //vendas de mercadorias produtos e servicos
            var l = b.VendasDeMercadoriasProdutosServicos();
            Console.WriteLine("VendasDeMercadoriasProdutosServicos => "+ l.ToString());
            //outras receitas
            var m = b.OutrasReceitas();
            Console.WriteLine("OutrasReceitas => "+m.ToString());
            //Provisao Para devedores duvidosos
            var n = b.ProvisaoParaDevedoresDuvidosos();
            Console.WriteLine("OutrasReceitas => " + n.ToString());
            //CustoDosProdutosMercadoriasEServicosVendidos()
            var p = b.CustoDosProdutosMercadoriasEServicosVendidos();
            Console.WriteLine("CustoDosProdutosMercadoriasEServicosVendidos => "+ p.ToString());
            //total
            Console.WriteLine("Total => "+ (l+m+n).ToString());



            Console.WriteLine("ACABOU");
            Console.ReadLine();
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

                //acabou de aplicar o zz 
                string clasct = classifconta.Substring(0, 2);
                if (clasct.Contains("4."))
                {
                    movcontactb = Math.Round((valorcred - valordeb), 2);

                }
                else if (clasct.Contains("5.") || clasct.Contains("6."))
                {
                    movcontactb = Math.Round((valordeb - valorcred), 2);
                }
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
                    MovContaCtb = movcontactb
                });

            }


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

        //busca os codigos "1.0.1.etc" na tabela PLANOPADRAO enquanto CONTACTB = x.y.w.z
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
                            var filtradas = saldomes.Where(c => c.Classifconta.Contains(clasfconta));

                            var CodigoempresaV = "";
                            var ContactbV = "";
                            var ClassifcontaV = "";
                            var DescrcontaV = "";
                            double MovContaCtbV = 0;
                            var NatursaldoV = "";
                            var TipocontaV = "";
                            //carrega no novo objeto o retorno da lista somado o MovContaCtb
                            foreach (var item in filtradas)
                            {
                                CodigoempresaV = item.Codigoempresa;
                                ContactbV = item.Contactb;
                                ClassifcontaV = item.Classifconta;
                                DescrcontaV = item.Descrconta;
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
                                MovContaCtbDva = MovContaCtbV
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
                            }

                            movicontdva.Add(new MovimentoContaDVA
                            {
                                CodigoPadraoDva = myReadertc["CODIGOPADRAO"].ToString(),
                                ContactbDva = myReadertc["CONTACTB"].ToString(),
                                TipocontaDva = myReadertc["TIPOCONTA"].ToString(),
                                ClassifcontaDva = myReadertc["CLASSIFCONTA"].ToString(),
                                DescrcontaDva = myReadertc["DESCRCONTA"].ToString(),
                                NatursaldoDva = myReadertc["NATURSALDO"].ToString(),
                                MovContaCtbDva = MovContaCtbV
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

        public double VendasDeMercadoriasProdutosServicos() {

            //vendas de mercadorias produtos e servicos
            double tolalzao = 0;
            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("2602") || item.ContactbDva.Contains("5007") || item.ContactbDva.Contains("5111") || item.ContactbDva.Contains("5103") || item.ContactbDva.Contains("5112"))
                {
                    tolalzao += Math.Abs(item.MovContaCtbDva);
                }
                else if (item.ContactbDva.Contains("2771") || item.ContactbDva.Contains("2798"))
                {
                    tolalzao -= Math.Abs(item.MovContaCtbDva);
                }
            }
            return tolalzao;
        }

        public double OutrasReceitas() {
            double tolalzao = 0;
            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("5419") || item.ContactbDva.Contains("5240") || item.ContactbDva.Contains("2375") || item.ContactbDva.Contains("4734"))
                {
                    tolalzao += Math.Abs(item.MovContaCtbDva);
                }
                
            }
            return tolalzao;
        }

        public double ProvisaoParaDevedoresDuvidosos() {
            double tolalzao = 0;
            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("4304") || item.ContactbDva.Contains("5483"))
                {
                    tolalzao += Math.Abs(item.MovContaCtbDva);
                }

            }
            return tolalzao;

        }

        public double CustoDosProdutosMercadoriasEServicosVendidos()
        {
            double tolalzao = 0;
            foreach (var item in movicontdva)
            {
                if (item.ContactbDva.Contains("3003") || item.ContactbDva.Contains("3008"))
                {
                    tolalzao += Math.Abs(item.MovContaCtbDva);
                }

            }
            return tolalzao;

        }




    }
}
