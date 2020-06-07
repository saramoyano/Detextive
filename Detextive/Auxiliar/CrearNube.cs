using Gma.CodeCloud.Controls.TextAnalyses.Blacklist;
using Gma.CodeCloud.Controls.TextAnalyses.Extractors;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Detextive.Auxiliar
{
    class CrearNube
    {
        private IBlacklist _blacklist;
        private IProgressIndicator _progress;
        
        int extension;
        public CrearNube()
        {
            _blacklist = _blacklist = new BannedWords();
            _progress = new ProgressBarWrapper();

        }

        public int Extension { get => extension; set => extension = value; }

        public IEnumerable<IWord> GenerarNube(string texto)
        {
            string textoNuevo = texto;
            if (textoNuevo.Length < 3)
            {
                return null;
            }
            IEnumerable<string> terms = new StringExtractor(textoNuevo, _progress);
            Extension = terms.Count();
            //CloudControl.WeightedWords = terms.Filter(_blacklist)
            //                                           .CountOccurences()
            //                                           .SortByOccurences();
            IEnumerable<IWord> terminos = terms.Filter(_blacklist)
                                                       .CountOccurences()
                                                       .SortByOccurences();
            return terminos;
        }



        internal class BannedWords : CommonBlacklist
        {
            private static readonly string[] s_TopCommonWords =
                new[]
                {"I", "a" ,
              "acá" ,
              "ahí" , "de", "la", "los", "las", "que", "el", "por", "se", "al" ,
                "algo" ,
                "algún" ,
                "alguna",
                "alguno",
                "algunas",
                "algunos",
                "allá" ,
                "allí" ,
                "ambos" ,
                "ante" ,  "aquí" ,
                "arriba" ,
                "así" ,
                "atrás" ,
                "aun" ,
                "aunque" ,
                "bajo" ,
                "bastante" ,
                "bien" ,
                "cabe" ,
                "cada" ,
                "casi" , "ciertos" ,
                "ciertas" ,
                "como" ,
                "con" ,
                "conmigo" ,
                "conseguimos" ,
                "conseguir" ,
                "consigo" ,
                "consigue" ,
                "consiguen" ,
                "consigues" ,
                "contigo" ,  "cualquieras" ,
                "cualesquiera" ,
                "cuan" ,
                "cuando" ,
                "cuanto" ,
                "cuanta" ,
                "cuantos" ,
                "cuantas" ,
                "de" ,
                "dejar" ,
                "del" ,
                  "desde" ,
                "donde" ,
                "dos" ,
                    "el",
                    "en" ,
                "encima" ,
                "entonces" ,
                "entre" ,
                "era",
                "eras" ,
                "eramos" ,
                "eran" ,
                "eres" ,
                "es" ,
                "esos" ,
                "esa" ,
                "eso" ,
                "ese" ,
                "esas" ,
                "estas" ,
                "esta" ,
                "estaba" ,
                "estado" ,
                "estáis" ,
                "estamos" ,
                "están" ,
                "estar" ,
                "este" ,
                "esto" ,
                "estos" ,
                "estés" ,
                "estoy" ,
                "etc" ,   "ha" ,
                    "han",
                    "haces" ,
                "hace" ,
                "hacéis" ,
                "hacemos" ,
                "hacen" ,
                "hacer" ,
                "hacia" ,
                "hago" ,
                "hasta" ,
                "incluso" ,
                "intentas" ,
                "intenta" ,
                "intentáis" ,
                "intentamos" ,
                "intentan" ,
                "intentar" ,
                "intento" ,
                "ir" ,
                "jamás" ,
                "juntos" ,
                "junto" ,
                "la" ,
                "lo" ,
                "las" ,
                "los" ,
                "largo" ,
                "más" ,
                "mas" ,
                "me" ,
                "menos" ,
                "mi" ,
                "mis" ,
                "mientras" ,
                "mío" ,
                "mía" ,
                "mías" ,
                "míos" ,  "modo" ,
                "mucha" ,
                "muchas" ,
                "muchos" ,
                "muchísima" ,
                "muchísimo" ,
                "muchísimas" ,
                "muchísimos" ,
                "mucho" ,
                "muchos" ,
                "muy" ,
                "nada" ,
                "ni" ,
                "ningún" ,
                "ninguna" ,
                "ninguno" ,
                "no" ,
                "nos" , "nunca" ,
                "os" ,
                    "otras",
                    "para" , "pero" ,"podéis" ,
                "podemos" ,
                "poder" ,
                "podría" ,
                "podrías" ,
                "podríais" ,
                "podríamos" ,
                "podrían" ,
                "por" ,
                "por qué" ,
                "porque" ,  "pueden" ,
                "puedes" ,
                "puede" ,
                "puedo" ,
                "pues" ,
                "que" ,
                "qué" ,
                "querer" ,
                "quienes" ,
                "quien" ,
                "quién" ,
                "quienesquiera" ,
                "quienquiera" ,
                "quizás" ,
                "quizá" ,
                    "se" ,
                "son",
                    "un",
                    "le",
                    "dijo",
                "según" ,
                "ser" ,
                "si" ,
                "sí" ,
                "sido",
                "siempre" ,
                "siendo" ,
                "sin" ,
                "sino" ,
                "so" ,
                "sobre" ,
                "sois" ,
                "solamente" ,
                "solo" ,
                "sólo" ,
                    "sr" ,
                "sra" ,
                "sres" ,
                "sta" ,
                "su" ,
                "sus" ,
                "suya" ,
                "suyo" ,
                "suyas" ,
                "suyos" ,
                "tal" ,
                "tales" ,
                "también" ,
                "tampoco" ,
                "tan" ,
                "tanta" ,
                "tanto" ,
                "tantas" ,
                "tantos" ,
                "te" ,
                "tenéis" ,
                "tenemos" ,
                "tener" ,
                "tengo" ,
                "ti" ,  "tú" ,
                "tu" ,
                "tus" ,
                "tuya " ,
                "tuyo" ,
                "tuyas" ,
                "tuyos" ,     "un " ,
                "una" ,
                "uno" ,
                "unas" ,
                "unos" ,
                "usa" ,
                "usas" ,
                "usáis" ,
                "usamos" ,
                "usan" ,
                "usar" ,
                "uso" ,
                "ustedes" ,
                "usted" ,
                "va" ,
                "van" ,
                "vais" ,
                "valor" ,
                "vamos" ,
                "varias" ,
                "varios" ,
                "vaya" ,
                    "voy" ,
                    "y" ,
                "ya" ,
                "yo",};

            public BannedWords() : base(s_TopCommonWords)
            {
            }
        }

        internal class ProgressBarWrapper : IProgressIndicator
        {
            private readonly ProgressBar m_ProgressBar;

            public ProgressBarWrapper()
            {
                m_ProgressBar = new ProgressBar();
            }

            public Double Value
            {
                get { return m_ProgressBar.Value; }
                set { m_ProgressBar.Value = value; }
            }

            public virtual Double Maximum
            {
                get { return m_ProgressBar.Maximum; }
                set { m_ProgressBar.Maximum = value; }
            }

            public virtual void Increment(int value)
            {
                // m_ProgressBar.Increment(value);
                // Application.DoEvents();
            }
        }
    }
}
