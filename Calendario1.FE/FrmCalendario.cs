using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendario1.FE
{
    public partial class FrmCalendario : Form
    {
        public FrmCalendario()
        {
            InitializeComponent();
        }

        private void FrmCalendario_Load(object sender, EventArgs e)
        {
            for(int f= 1; f <=96; f++)
            {
                dataGridView1.Rows.Add();
            }
            CargarFecha();
        }

        private void CargarFecha()
        {
            DateTime select = monthCalendar1.SelectionStart;
            label1.Text="Fecha seleccionada:"+select.ToString("dd/MM/yyyy");
            string fecha=select.Year.ToString()+select.Month.ToString()+select.Day.ToString();
            if(!File.Exists(fecha) ) 
            {
                StreamWriter archivo = new StreamWriter(fecha);
                DateTime fe = DateTime.Today;
                for(int f=1; f <=96;f++)
                {
                    archivo.WriteLine(fe.ToString("HH:mm"));
                    archivo.WriteLine("");
                    fe = fe.AddMinutes(15);
                }
                archivo.Close();
            }
            StreamReader archivo2 = new StreamReader(fecha);
            int x = 0;
            while(!archivo2.EndOfStream) 
            {
                string linea1 = archivo2.ReadLine();
                string linea2 = archivo2.ReadLine();
                dataGridView1.Rows[x].Cells[0].Value = linea1;
                dataGridView1.Rows[x].Cells[1].Value = linea2;
                x++;
            }
            archivo2.Close();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            CargarFecha();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DateTime select = monthCalendar1.SelectionStart;
            string fecha = select.Year.ToString() + select.Month.ToString() + select.Day.ToString();
            StreamWriter archivo = new StreamWriter(fecha);
            for (int f=0;f<dataGridView1.Rows.Count; f++) 
            {
                archivo.WriteLine(dataGridView1.Rows[f].Cells[0].Value.ToString());
                if (dataGridView1.Rows[f].Cells[1].Value != null)
                    archivo.WriteLine(dataGridView1.Rows[f].Cells[1].Value.ToString());
                else
                    archivo.WriteLine("");
            }
            archivo.Close();
        }
    }
}
