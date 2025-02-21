using System.Diagnostics;

namespace WF.AimTest
{
    public partial class Form1 : Form
    {
        // Local de
        // variaveis
        // propriedades

        // 2 Buttons
        private Button btnIniciar;
        private Button btnAlvo;

        // timer
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timer2;

        // random
        private Random random;

        // Stopwatch
        private Stopwatch stopwatch;
        private int qtdClick = 0;




        // construtor da tela
        public Form1()
        {
            InitializeComponent();

            // determina o titulo da tela
            this.Text = "Aim Tester";
            // determina largura e altura
            this.Size = new Size(500, 500);
            // determina a posi��o inicial da tela -> nesse caso centralizada
            this.StartPosition = FormStartPosition.CenterParent;

            btnIniciar = new Button()
            {
                Text = "iniciar",
                Size = new Size(100, 50),
            }; 

             btnIniciar.Click += IniciarJogo;
            //adiciona o bot�o na tela;

            this.Controls.Add(btnIniciar);

            this.btnAlvo = new Button()
            {
                Size = new Size(50, 50),
                BackColor = Color.Red,
                Visible = false,
            };

            btnAlvo.Click += btnAlvoClick;
                
            // adiciona botao alvo na tela
            this.Controls.Add(btnAlvo);


            timer = new System.Windows.Forms.Timer();
            timer.Tick += MostrarBotaoAlvo;


            random = new Random();
            stopwatch = new Stopwatch();

            // fim construtor




        }

        // methods

        private void IniciarJogo(object sender, EventArgs e)
        {
            // desabilita o bota�o 
            btnIniciar.Enabled = false;
            IniciarNovaRodada();
        }

        private void IniciarNovaRodada()
        {
            timer.Interval = random.Next(1000, 3000);
            timer.Start();
        }

        private void MostrarBotaoAlvo(object sender,EventArgs e)
        {
            // para o timer
            timer.Stop();
            
            int x = random.Next(50, this.ClientSize.Width - 70);
            int y = random.Next(50, this.ClientSize.Height - 70);

            btnAlvo.Location = new Point(x, y);

            btnAlvo.Visible = true;
            stopwatch.Restart();
        }
            

        private void btnAlvoDoubleCLick(object sender, EventArgs e)
        {
            stopwatch.Stop();

            btnAlvo.Visible = false;
            MessageBox.Show($"Tempo de rea��o: {stopwatch.ElapsedMilliseconds}", "ms");
            Task.Delay(500).ContinueWith(_ => IniciarNovaRodada(),
                TaskScheduler.FromCurrentSynchronizationContext());
        }


        private void btnAlvoClick(object sender, EventArgs e)
        {
            timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 1000;
            timer2.Start();
            
            qtdClick += 1;

            timer2.Tick += falha;

            if(qtdClick == 2)
            {
                stopwatch.Stop();
                btnAlvo.Visible = false;
                qtdClick = 0;
                timer2.Stop();
                MessageBox.Show($"Tempo de rea��o: {stopwatch.ElapsedMilliseconds}", "ms");
                Task.Delay(500).ContinueWith(_ => IniciarNovaRodada(),
                    TaskScheduler.FromCurrentSynchronizationContext());

               
            }

        }

        private void falha(object sender, EventArgs e)
        {
            timer2.Stop();
            stopwatch.Stop();
            qtdClick = 0;
            btnAlvo.Visible = false;
            MessageBox.Show("Voc� n�o deu clique duplo e falhou, seu lixo","lixo");
            Task.Delay(500).ContinueWith(_ => IniciarNovaRodada(),
                TaskScheduler.FromCurrentSynchronizationContext());

        }


    }
}
