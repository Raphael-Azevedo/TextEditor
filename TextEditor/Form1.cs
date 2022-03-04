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

namespace TextEditor
{
    public partial class Form1 : Form
    {
        StringReader leitura = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Novo()
        {
            DialogResult confirm = MessageBox.Show("Deseja Continuar?", "Salvar Arquivo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            if (confirm.ToString().ToUpper() == "YES")
            {
                Salvar();
            }
            richTextBox1.Clear();
            richTextBox1.Focus();
        }

        private void Salvar()
        {
            try
            {
                if(this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileStream arquivo = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter streamWriter = new StreamWriter(arquivo);
                    streamWriter.Flush();
                    streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                    streamWriter.Write(this.richTextBox1.Text);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Erro na gravação: " + ex.Message,"Error ao gravar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Abrir()
        {
            this.openFileDialog1.Title = "Abrir Arquivo";
            openFileDialog1.InitialDirectory = "C:";
            openFileDialog1.Filter = "(*.txt)|*.txt";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream arquivo = new FileStream(openFileDialog1.FileName,FileMode.Open, FileAccess.Read);
                    StreamReader streamReader = new StreamReader(arquivo);
                    streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    this.richTextBox1.Text = "";
                    string linha = streamReader.ReadLine();
                    while(linha != null)
                    {
                        this.richTextBox1.Text += linha + "\n";
                        linha = streamReader.ReadLine();
                    }
                    streamReader.Close();
                }   
                catch(Exception ex)
                {
                    MessageBox.Show("Erro de leitura: " + ex.Message, "Error ao ler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Copiar()
        {
            if(richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void Colar()
        {
            richTextBox1.Paste();
        }
     
        private void Negrito()
        {
            Define_estilo_da_fonte("Bold");
        }

        private void Italico()
        {
            Define_estilo_da_fonte("Italic");
        }

        private void Sublinhado()
        {
            Define_estilo_da_fonte("Underline");
        }

        private void Define_estilo_da_fonte(string nome_estilo_fonte_selecionado)
        {
            Dictionary<String, FontStyle> fontes = new Dictionary<String, FontStyle>();

            fontes.Add("r", FontStyle.Regular);

            fontes.Add("b", FontStyle.Bold);
            fontes.Add("i", FontStyle.Italic);
            fontes.Add("u", FontStyle.Underline);

            fontes.Add("biu", FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);

            fontes.Add("bi", FontStyle.Bold | FontStyle.Italic);
            fontes.Add("bu", FontStyle.Bold | FontStyle.Underline);
            fontes.Add("iu", FontStyle.Italic | FontStyle.Underline);

            string chave_estilo_fonte = "";

            List<string> nomes_estilo_fonte = new List<string>();

            nomes_estilo_fonte.Add("Bold");
            nomes_estilo_fonte.Add("Italic");
            nomes_estilo_fonte.Add("Underline");


            foreach (string nome_estilo_fonte in nomes_estilo_fonte)
            {
                if ((richTextBox1.SelectionFont.Style.ToString().IndexOf(nome_estilo_fonte) != -1) && (nome_estilo_fonte != nome_estilo_fonte_selecionado))
                {
                    chave_estilo_fonte += nome_estilo_fonte.Substring(0, 1).ToLower();
                }
                else if ((richTextBox1.SelectionFont.Style.ToString().IndexOf(nome_estilo_fonte) == -1) && nome_estilo_fonte == nome_estilo_fonte_selecionado)
                {
                    chave_estilo_fonte += nome_estilo_fonte.Substring(0, 1).ToLower();
                }
            }

            if (chave_estilo_fonte.Length == 0)
                chave_estilo_fonte = "r";


            string nome_da_fonte = richTextBox1.SelectionFont.Name;
            float tamanho_da_fonte = richTextBox1.SelectionFont.Size;

            Font fonte = new Font(nome_da_fonte, tamanho_da_fonte, fontes[chave_estilo_fonte]);

            richTextBox1.SelectionFont = fonte;

        }
        private void AlinharEsquerda()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }
        private void AlinharDireita()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }
        private void AlinharCentro()
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void Imprimir()
        {
            printDialog1.Document = printDocument1;
            string texto = this.richTextBox1.Text;
            leitura = new StringReader(texto);
            if(printDialog1.ShowDialog()== DialogResult.OK)
            {
                this.printDocument1.Print();
            }

        }

        private void Btn_negrito_Click(object sender, EventArgs e)
        {
            Negrito();
        }

        private void Btn_italico_Click(object sender, EventArgs e)
        {
            Italico();
        }

        private void Btn_sublinhado_Click(object sender, EventArgs e)
        {
            Sublinhado();
        }

        private void negritoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Negrito();
        }

        private void italicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Italico();
        }

        private void sublinhadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sublinhado();
        }

        private void Btn_esquerda_Click(object sender, EventArgs e)
        {
            AlinharEsquerda();
        }

        private void esquerdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlinharEsquerda();
        }

        private void direitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlinharDireita();
        }

        private void Btn_direita_Click(object sender, EventArgs e)
        {
            AlinharDireita();
        }

        private void Btn_centro_Click(object sender, EventArgs e)
        {
            AlinharCentro();
        }

        private void centralizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlinharCentro();
        }

        private void Btn_novo_Click(object sender, EventArgs e)
        {
            Novo();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Novo();
        }
        private void Btn_salvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salvar();
        }
        private void Btn_abrir_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir();
        }
        private void Btn_copiar_Click(object sender, EventArgs e)
        {
            Copiar();
        }

        private void Btn_colar_Click(object sender, EventArgs e)
        {
            Colar();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copiar();
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Colar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float linhasPagina = 0;
            float PosY = 0;
            int cont = 0;
            float MargemEsquerda = e.MarginBounds.Left - 50;
            float MargemSuperior = e.MarginBounds.Top - 50;

            if (MargemEsquerda < 5)
            {
                MargemEsquerda = 20;
            }
            if (MargemSuperior < 5)
            {
                MargemSuperior = 20;
            }
            string linha = null;
            Font fonte = this.richTextBox1.Font;
            SolidBrush pincel = new SolidBrush(Color.Black);
            linhasPagina = e.MarginBounds.Height / fonte.GetHeight(e.Graphics);
            linha = leitura.ReadLine();
            while (cont < linhasPagina)
            {
                PosY = (MargemSuperior + (cont * fonte.GetHeight(e.Graphics)));
                e.Graphics.DrawString(linha, fonte, pincel, MargemEsquerda, PosY, new StringFormat());
                cont++;
                linha = leitura.ReadLine();
            }
            if (linha != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
            pincel.Dispose();
        }

        private void Btn_Imprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
