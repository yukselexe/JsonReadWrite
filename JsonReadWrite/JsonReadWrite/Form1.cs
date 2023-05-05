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
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Masaüstü konumunu path deðiþkenine kaydettim
            string dosya_yolu = path + "\\settings.json"; //Masaüstünde settings.json dosyasýnýn veri çekeceðimiz dosya olduðunu dosya_yolu deðiþkenine kaydettim

            if (System.IO.File.Exists(dosya_yolu)) //eðer dosya belirtilen konumda var ise iþlemlere devam et
            {
                using (StreamReader r = new StreamReader(dosya_yolu)) //StreamReader ile dosya okuma iþlemi
                {
                    string json = r.ReadToEnd(); //json deðiþkenine dosyanýn tüm içeriðini kaydettim
                    List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json); //json formatýný liste olarak çevirme
                    dynamic array = JsonConvert.DeserializeObject(json); //Ýçerikleri diziye aktarma
                    foreach (var item in array)
                    {
                        label1.Text = item.data1; //Jsondaki data1 datasýnýn karþýsýndaki veriyi label1'e yazdýrdým
                        label2.Text = item.data2; //Jsondaki data2 datasýnýn karþýsýndaki veriyi label2'e yazdýrdým
                    }
                }
            }
            else //belirtilen konumda dosya yok ise oluþtur ve fonksiyonu yeniden çalýþtýr
            {
                FileStream fs = File.Create(dosya_yolu); //dosya oluþtur
                fs.Close(); //Fonksiyonu tekrar çaðýrdýðýmýzda "zaten kullanýmda" hatasý almamak için FileStream durumunu kapatýyoruz.
                Item renkdata = new Item(); //Item Class içerisinden Default verileri oku


                string JSONresult = JsonConvert.SerializeObject(renkdata); //Bu verileri Json'a çevir

                using (var tw = new StreamWriter(dosya_yolu, true)) { tw.WriteLine("[ \n" + JSONresult.ToString() + "\n ]"); tw.Close(); } //Json dosyasýna default verileri yaz.

                
                Thread.Sleep(1000); //1 saniyeliðine beklet
                LoadJson(); //fonksiyonu çaðýr
            }





        }

        public class Item
        {
            public string data1 = "This is Json Data 1"; //Item class'ý oluþturdum ve jsondaki verilerin baþlýklarýný public deðiþken olarak atadým.
            public string data2 = "This is Json Data 2";

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadJson(); //Form açýldýðýnda verileri otomatik çekmesi için Load eventine fonksiyonu ekledim.




        }
    }
}