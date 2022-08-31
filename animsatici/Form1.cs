using Newtonsoft.Json;
using System.Media;

namespace animsatici
{
    public partial class Form1 : Form
    {
        List<Reminding> remindings = new List<Reminding>();
        public Form1()
        {
            InitializeComponent();
        }

        int a = 0;

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        bool isShowReminding = false;
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (DateTime.Now.Hour == 17 && isShowReminding == false)
            {
                showReminding("Github ve bilgisayar \nyedeğinizi almayı unutmayın.!");
                isShowReminding = true;
            }
            else if (DateTime.Now.Hour != 17)
            {
                isShowReminding = false;
            }



            foreach (var reminding in remindings)
            {
                if (reminding.isActive == true)
                {
                    if (reminding.dateTime < DateTime.Now)
                    {

                        showReminding(reminding.message);
                        reminding.isActive = false;

                        saveReminding(remindings);
                    }

                }
            }
        }
        public string mesaj = " ";
        private void Form1_Load(object sender, EventArgs e)
        {
            remindings = readReminding();
            dateTimePicker1.Value = DateTime.Now;
            timer1.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {


            Reminding reminding = new Reminding();
            reminding.dateTime = dateTimePicker1.Value;
            reminding.message = textBox1.Text.Trim();
            reminding.isActive = true;
            remindings.Add(reminding);


            showMessage("Hatırlatma kaydedildi!");
            saveReminding(remindings);

        }


        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            showMessage("Hatırlatma durduruldu!");

            if (comboBox1.SelectedItem != null)
            {
                var reminding = remindings.FirstOrDefault(i => i.message == comboBox1.SelectedItem.ToString());
                if (reminding != null)
                {
                    remindings.Remove(reminding);
                }
            }
            else
            {
                showMessage("Lütfen seçim yapınız!");
            }

            saveReminding(remindings);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void showMessage(string message)
        {
            label3.Visible = true;
            label3.Text = message;
            Task.Run(() =>
            {
                Thread.Sleep(3000);
                label3.Invoke(new Action(() =>
                {

                    label3.Visible = false;
                }));
            });
        }
        public static string readFile()
        {
            string dosya_yolu = Application.StartupPath + "/remindings.json";
            //Okuma işlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosyanın açılacağını,
            //3.parametre dosyaya erişimin veri okumak için olacağını gösterir.
            StreamReader sw = new StreamReader(fs);
            //Okuma işlemi için bir StreamReader nesnesi oluşturduk.
            string yazi = sw.ReadToEnd();
            //Satır satır okuma işlemini gerçekleştirdik ve ekrana yazdırdık
            //Son satır okunduktan sonra okuma işlemini bitirdik
            sw.Close();
            fs.Close();
            //İşimiz bitince kullandığımız nesneleri iade ettik.

            return yazi;
        }
        private static void saveFile(string value)
        {
            string dosya_yolu = Application.StartupPath + "/remindings.json";
            //İşlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
            //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
            StreamWriter sw = new StreamWriter(fs);
            //Yazma işlemi için bir StreamWriter nesnesi oluşturduk.
            sw.Write(value);
            //Dosyaya ekleyeceğimiz iki satırlık yazıyı WriteLine() metodu ile yazacağız.
            sw.Flush();
            //Veriyi tampon bölgeden dosyaya aktardık.
            sw.Close();
            fs.Close();
            //İşimiz bitince kullandığımız nesneleri iade ettik.
        }
        private void saveReminding(List<Reminding> remindings)
        {
            string json = JsonConvert.SerializeObject(remindings);
            saveFile(json);

        }

        private List<Reminding> readReminding()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Reminding>>(readFile());
            }
            catch (Exception)
            {
                return new List<Reminding>();
            }

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }

        private void showReminding(string message)
        {
            frmMessage frm = new frmMessage();
            frm.mesaj = message;
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height);
            frm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (var Reminding in remindings)
            {
                comboBox1.Items.Add(Reminding.message);
            }
        }
    }
}