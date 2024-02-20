using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
// COMENTARI CHORRA
namespace PROVA_TEORICA
{
    class Program
    {
        static void Main(string[] args)
        {
            List<String> missatges = CarregarMissatges("MISSATGES.TXT");
            missatges = EnviarMissatges(missatges);
            LlistarMissatgesCorrectes(missatges);
        }

       
        public static List<String> EnviarMissatges(List<String> missatges)
        {
            missatges[5] = "01111111";
            missatges[4] = "10000000";
            return missatges;
        }

        public static int[] StringToArrayDeEnters(String missatgeStr)
        {
            int[] missatge = new int[missatgeStr.Length];
            for (int i = 0; i < missatge.Length; i++)
                missatge[i] = Convert.ToInt32(missatgeStr[i])-48;
            return missatge;
        }
        public static int Paritat(int[] missatge7Bits)
        {
            int suma = 0;
            if (missatge7Bits.Length != 7) throw new Exception("LONGITUD DE MISSATGE INCORRECTA");
            for (int i=0;i<missatge7Bits.Length;i++)
            {
                if (missatge7Bits[i] == 1) suma++;
                else if (missatge7Bits[i] != 0) throw new Exception("FORMAT DE MISSATGE INCORRECTE!");
            }
            return suma%2;
        }

        public static String PreparaMissatgeAmb8Bits(int [] missatge7Bits)
        {
            StringBuilder sBMissatge = new StringBuilder();
            String missatgeFinal = null;
            try
            {
                int paritat = Paritat(missatge7Bits);
                for (int i = 0; i < missatge7Bits.Length; i++) sBMissatge.Append(missatge7Bits[i]);
                sBMissatge.Append(paritat);
                missatgeFinal = sBMissatge.ToString();
            }
            catch(Exception e)
            {
                missatgeFinal = null;
            }
            return missatgeFinal;
        }

        public static List<String> CarregarMissatges(String fileName)
        {
            int[] missatge7Bits;
            String missatge8Bits;
            List<String> llistaMsg = new List<String>();
            StreamReader srMissatges = new StreamReader(fileName);
            string linia = srMissatges.ReadLine(); ;
            while (linia!=null)
            {
                missatge7Bits = StringToArrayDeEnters(linia);
                missatge8Bits = PreparaMissatgeAmb8Bits(missatge7Bits);
                if (missatge8Bits!=null)
                {
                    llistaMsg.Add(missatge8Bits);
                }
                linia = srMissatges.ReadLine();
            }
            return llistaMsg;
        }

        public static bool MissatgeCorrecte(String missatge)
        {
            bool correcte = true;
            int[] missatgeInt = StringToArrayDeEnters(missatge);
            int suma = 0;
            for (int i=0;i<missatgeInt.Length-1;i++)
            {
                suma = suma + missatgeInt[i];
            }

            if (suma % 2 == 0 && missatgeInt[missatgeInt.Length - 1] == 1 || suma % 2 == 1 && missatgeInt[missatgeInt.Length - 1] == 0)
                correcte = false;
            return correcte;

        }

        public static String EliminaBit8(String missatge)
        {
            StringBuilder sb = new StringBuilder();
            for (int i=0;i<missatge.Length-1;i++)
            {
                sb.Append(missatge[i]);
            }
            return sb.ToString();
        }

        public static void LlistarMissatgesCorrectes(List<String> missatges)
        {
            foreach (String msg in missatges)
            {
                if (MissatgeCorrecte(msg)) Console.WriteLine(EliminaBit8(msg));
            }
        }

    }
}
