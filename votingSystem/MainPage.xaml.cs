using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;



namespace votingSystem
{
    [Serializable]
    public struct RSAParametersVoting
    {
        
        public string Exponent;
        
        public string Modulus;
       
    }
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Clé privée en string.
        /// </summary>
        public static string privateKey = "lkjgmlkjglkdfmjgmklfjdslgknf,slivoaindksnflqljlmjmlkjmdkjsqfkdjsfoidjoijfoizjfnlsmqdnklnfk";

        /// <summary>
        /// Clé public en string.
        /// </summary>
        public static string publicKey = "kldfqsjlkdjfoioionzfgrezgfqdfsqfegtzrzrzrg";

        /// <summary>
        /// Clé public en byte.
        /// </summary>
        byte[] bytePublicKey;

        /// <summary>
        /// Clé pulic en string base 64.
        /// </summary>
        string base64Public;

        /// <summary>
        /// CLé privée en byte.
        /// </summary>
        byte[] bytePrivateKey;

        /// <summary>
        /// CLé privée en string.
        /// </summary>
        string base64Private;


        /// <summary>
        /// Text clair.
        /// </summary>
        public static string textclair = "";

        /// <summary>
        /// Text chiffrer.
        /// </summary>
        public static string textChiffrer = "";



        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Méthode appélé pour générer les clés.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GenerationPairkey(object sender, EventArgs e)
        {
            using (var rsa = new RSACryptoServiceProvider(1024))
            {

                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
                Console.WriteLine($"XML String : {publicKey}");

                bytePublicKey = Encoding.UTF8.GetBytes(publicKey.ToString());
                base64Public = Convert.ToBase64String(bytePublicKey);

                bytePrivateKey = Encoding.UTF8.GetBytes(privateKey);
                base64Private = Convert.ToBase64String(bytePrivateKey);
            }
            Console.WriteLine($"public = {base64Public}/n private = {base64Private}");

            privateKeyEditor.Text = base64Private.ToString();
            publicKeyEditor.Text = base64Public.ToString();

        }

        /// <summary>
        /// Méthode qui chiffre le message.
        /// </summary>
        public void EncryptMessage(object sender, EventArgs e)
        {
            textclair = textClairEditor.ToString();
            if (string.IsNullOrEmpty(textclair))
                return;

            // Transformation de la clé de la base 64 en tableau de byte.
            byte[] publickeyTobase64ToByte = Convert.FromBase64String(base64Public);

            // Transformation de la clé du tableau de byte en string.
            
            string publicKeyStringTem = Encoding.UTF8.GetString(publickeyTobase64ToByte);
            using (var rsa = new RSACryptoServiceProvider())
            {
                //rsa.ImportParameters(publicKeyStringTem);
                //rsa.FromXmlString(publicKeyStringTem);
                XmlSerializer serializer = new XmlSerializer(typeof(RSAParametersVoting));
                using (TextReader reader = new StringReader(publicKeyStringTem))
                {
                    Console.WriteLine($"reader: {reader.ToString()}");

                    RSAParametersVoting param = (RSAParametersVoting)serializer.Deserialize(reader);
                    Console.WriteLine($"Modulus: {param.Modulus}");

                }
            }

        }


    }
}