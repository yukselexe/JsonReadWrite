using Newtonsoft.Json;

namespace JsonReadWrite

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void LoadJson() //json veri okuma 
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Masa�st� konumunu path de�i�kenine kaydettim
            string dosya_yolu = path + "\\settings.json"; //Masa�st�nde settings.json dosyas�n�n veri �ekece�imiz dosya oldu�unu dosya_yolu de�i�kenine kaydettim

            if (System.IO.File.Exists(dosya_yolu)) //e�er dosya belirtilen konumda var ise i�lemlere devam et
            {
                using (StreamReader r = new StreamReader(dosya_yolu)) //StreamReader ile dosya okuma i�lemi
                {
                    string json = r.ReadToEnd(); //json de�i�kenine dosyan�n t�m i�eri�ini kaydettim
                    List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json); //json format�n� liste olarak �evirme
                    dynamic array = JsonConvert.DeserializeObject(json); //��erikleri diziye aktarma
                    foreach (var item in array)
                    {
                        label1.Text = item.data1; //Jsondaki data1 datas�n�n kar��s�ndaki veriyi label1'e yazd�rd�m
                        label2.Text = item.data2; //Jsondaki data2 datas�n�n kar��s�ndaki veriyi label2'e yazd�rd�m
                    }
                }
            }
            else //belirtilen konumda dosya yok ise olu�tur ve fonksiyonu yeniden �al��t�r
            {
                FileStream fs = File.Create(dosya_yolu); //dosya olu�tur
                fs.Close(); //Fonksiyonu tekrar �a��rd���m�zda "zaten kullan�mda" hatas� almamak i�in FileStream durumunu kapat�yoruz.
                Item renkdata = new Item(); //Item Class i�erisinden Default verileri oku


                string JSONresult = JsonConvert.SerializeObject(renkdata); //Bu verileri Json'a �evir

                using (var tw = new StreamWriter(dosya_yolu, true)) { tw.WriteLine("[ \n" + JSONresult.ToString() + "\n ]"); tw.Close(); } //Json dosyas�na default verileri yaz.

                
                Thread.Sleep(1000); //1 saniyeli�ine beklet
                LoadJson(); //fonksiyonu �a��r
            }





        }

        public class Item
        {
            public string data1 = "This is Json Data 1"; //Item class'� olu�turdum ve jsondaki verilerin ba�l�klar�n� public de�i�ken olarak atad�m.
            public string data2 = "This is Json Data 2";

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadJson(); //Form a��ld���nda verileri otomatik �ekmesi i�in Load eventine fonksiyonu ekledim.




        }
    }
}