
using System.Linq.Expressions;
using System.Reflection;
namespace UTS_3;

    internal class Program
    {
        static void Main(string[] args)
        {
            //Inisiasi 
            int papan = 5;
            int jumlahTikus = 3;
            char tikus = 'R';
            char salah = 'O';
            char kosong = '-';
            char kena = 'D';
            char tikusMati = 'X';
            Console.WriteLine("Selamat datang di Game Tebak Tikus yang tidak berdasi!");
            Console.WriteLine("Masukkan angka 1-5 pada kolom atau baris lalu Enter !");

            //Tempat Tikus
            char[,] Area = lokasiTikus(papan, kosong, tikus, jumlahTikus);
            printtikuss(Area, kosong, tikus, tikusMati);

            //Tikus hidup
            int tikusHidup = jumlahTikus;

            //Erroring
            while (tikusHidup > 0){
                try{
                    int[] tebakan = TampilkanPapan(papan);
                    char setelahDitebak = cekTebakan(tebakan, Area, tikus, kosong, kena, salah, tikusMati);
                       if (setelahDitebak == kena){
                        tikusHidup--;
                         }
                    Area = setelahTebakan(Area, tebakan, setelahDitebak);
                    Console.WriteLine("Tikus tersisa :" + tikusHidup);
                    printtikuss(Area, kosong, tikus, tikusMati);
                 }
                catch (Exception){
                Console.Clear();
                Console.WriteLine("Terjadi error!");  
                continue;
                }
            }
        }        
        //Array 
        private static void printtikuss(char[,] Area, char kosong, char tikus, char tikusMati)
        {
            Console.Write("  ");
            for (int i = 0; i < 5; i++){
                Console.Write(i + 1 + " ");
            }
            Console.WriteLine();
            for (int baris = 0; baris < 5; baris++){
                Console.Write(baris + 1 + " ");
                for (int kolom = 0; kolom < 5; kolom++){
                    char posisi = Area[baris, kolom];
                    if (posisi == tikus){
                        Console.Write(kosong + " ");
                    }
                    else{
                        Console.Write(posisi + " ");
                    }
                }
                Console.WriteLine();
            }
        }
        
        //Pemanggilan tikus
        private static char[,] AdaTikus(char[,] Area, int jumlahTikus, char kosong, char tikus)
        {
            int letakTikus = 0;
            int papan = 5;

            while (letakTikus < jumlahTikus){
                int[] lokasiTikus = rngTikus(papan);
                char posisi = Area[lokasiTikus[0], lokasiTikus[1]];
                 if (posisi == kosong){
                    Area[lokasiTikus[0], lokasiTikus[1]] = tikus;
                    letakTikus++;
                }
            }
            return Area;
        } 
        //rng Tikus
        private static int[] rngTikus(int papan)
        {
            Random rnd = new Random();
            int[] peletakan = new int[2];
            for (int i = 0; i < peletakan.Length; i++)
            {
                peletakan[i] = rnd.Next(papan);
            }
            return peletakan;
        }
        // Tampilkan di papan
        private static int[] TampilkanPapan(int papan)
        {   
            int baris;
            int kolom;
            do{
                Console.WriteLine(" ");
                Console.Write("Pilih baris : ");
                baris = Convert.ToInt32(Console.ReadLine());
            }
            while (baris < 0 || baris > papan + 1);
            do{
                Console.Write("Pilih kolom: ");
                kolom = Convert.ToInt32(Console.ReadLine());
            }
            while (kolom < 0 || kolom > papan + 1);
            return new[] { baris - 1, kolom - 1 };
        }
        //cek tebakan pemain
        private static char cekTebakan(int[] tebakan,char[,] Area, char tikus, char kosong,char kena, char salah,char tikusMati)                           
        {
            string message;
            int baris = tebakan[0];
            int kolom = tebakan[1];
            char target = Area[baris, kolom];
             if (target == tikus){
                Console.WriteLine(" ");
                message = "Selamat anda telah membunuh tikus yang tak berdasi!";
                Console.WriteLine(" ");
                target = kena;
            }
            else if (target == kosong){
                Console.WriteLine(" ");
                message = "Anda belum membunuh tikus yang tak berdasi !";
                Console.WriteLine(" ");
                target = salah;
            }
            else if (target == kena){
                Console.WriteLine(" ");
                message = " Tikus tidak berdasi telah mati! ";
                target = tikusMati;
            }
            else{
                Console.WriteLine(" ");
                message = " Kebun ini sudah bebas dari tikus yang tak berdasi ! ";
                Console.WriteLine(" ");
            }
            Console.WriteLine(message);
            return target;
        }


        //setelah menebak
        private static char[,] setelahTebakan(char[,] Area, int[] tebakan, char setelahDitebak)
        {
            int baris = tebakan[0];
            int kolom = tebakan[1];
            Area[baris, kolom] = setelahDitebak;
            return Area;
        }

       
        //lokasi tikus
        private static char[,] lokasiTikus(int papan, char kosong, char tikus, int jumlahTikus)
        {
            char[,] Area = new char[papan, papan];
            for (int baris = 0; baris < papan; baris++){
                for (int kolom = 0; kolom < papan; kolom++){
                    Area[baris, kolom] = kosong;
                }
            }
            return AdaTikus(Area, jumlahTikus, kosong, tikus);
        }  
    }
