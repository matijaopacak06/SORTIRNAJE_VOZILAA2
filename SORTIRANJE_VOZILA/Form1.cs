using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;


namespace SORTIRANJE_VOZILA
{
    public partial class Form1 : Form
    {
        private List<Vozilo> vozila = new List<Vozilo>();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnDodajVozilo_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox3.Text, out int godina) && int.TryParse(textBox4.Text, out int kilometraza) && godina > 0 && kilometraza >= 0)
            {
                Vozilo novoVozilo = new Vozilo()
                {
                    Marka1 = textBox1.Text,
                    Model1 = textBox2.Text,
                    GodinaProizvodnje1 = godina,
                    Kilometraza1 = kilometraza

                };
                vozila.Add(novoVozilo);

            }
            else
            {
                MessageBox.Show("Unesite validne podatke.");
            }


        }

        private void btnSortirajVozilo_Click(object sender, EventArgs e)
        {
            var kriterij = comboBox1.SelectedItem.ToString();
            var sortiranaVozila = vozila.AsEnumerable();



            if (comboBox1.SelectedItem.ToString() == "Marka")
                sortiranaVozila = vozila.OrderBy(v => v.Marka1).ToList();
            else if (comboBox1.SelectedItem.ToString() == "Model")
                sortiranaVozila = vozila.OrderBy(v => v.Model1).ToList();
            else if (comboBox1.SelectedItem.ToString() == "GodinaProizvodnje")
                sortiranaVozila = vozila.OrderBy(v => v.GodinaProizvodnje1).ToList();
            else if (comboBox1.SelectedItem.ToString() == "Kilometraza")
                sortiranaVozila = vozila.OrderBy(v => v.Kilometraza1).ToList();

            textBox5.Clear();
            foreach (var vozilo in sortiranaVozila)
            {
                textBox5.AppendText($"{vozilo.Marka1} - {vozilo.Model1} - {vozilo.GodinaProizvodnje1} - {vozilo.Kilometraza1}\n");
            }
        }

        private void btnObrisiVozilo_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems != null)
            {
                vozila.RemoveAt(listBox1.SelectedIndex);
                PrikaziiVozila();
            }

        }

        private void PrikaziVozila_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PrikaziiVozila()
        {
            listBox1.Items.Clear();
            foreach (var vozilo in vozila)
            {
                listBox1.Items.Add($"{vozilo.Marka1} - {vozilo.Model1} - {vozilo.GodinaProizvodnje1} - {vozilo.Kilometraza1}");
            }
        }

        private void UpdateVozilaList()
        {
            listBox1.Items.Clear();
            foreach (var vozilo in vozila)
            {
                listBox1.Items.Add(vozilo.ToString());
            }


            MessageBox.Show($"Broj vozila u listi: {vozila.Count}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void SpremiUXML()
        {
            try
            {
                var xmlDoc = new XDocument(
                    new XElement("Vozila",
                        vozila.Select(v =>
                            new XElement("Vozilo",
                                new XElement("Marka", v.Marka1),
                                new XElement("Model", v.Model1),
                                new XElement("GodinaProizvodnje", v.GodinaProizvodnje1),
                                new XElement("Kilometraza", v.Kilometraza1)
                            )
                        )
                    )
                );

                xmlDoc.Save("vozila.xml");
                MessageBox.Show("Podaci su uspješno spremljeni u XML datoteku.", "Uspjeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška prilikom spremanja: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }

        private void UcitajXML()
        {

            try
            {
                if (File.Exists("vozila.xml"))
                {
                    var xmlDoc = XDocument.Load("vozila.xml");
                    vozila = xmlDoc.Descendants("Vozilo")
                        .Select(v => new Vozilo
                        {
                            Marka1 = v.Element("Marka")?.Value,
                            Model1 = v.Element("Model")?.Value,
                            GodinaProizvodnje1 = int.Parse(v.Element("GodinaProizvodnje")?.Value ?? "0"),
                            Kilometraza1 = int.Parse(v.Element("Kilometraza")?.Value ?? "0")
                        }).ToList();

                    UpdateVozilaList();
                    MessageBox.Show("Podaci su uspješno učitani iz XML datoteke.", "Uspjeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("XML datoteka ne postoji.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška prilikom učitavanja: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        string filePath = "vozila.xml";

        private void button1_Click(object sender, EventArgs e)
        {
            SpremiUXML();


        }

        private void btnUcitajXML_Click(object sender, EventArgs e)
        {


            UcitajXML();



        }
        
    }

}
