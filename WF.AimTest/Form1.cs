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

        // random
        private Random random;

        // Stopwatch
        private Stopwatch stopwatch;





        // construtor da tela
        public Form1()
        {
            InitializeComponent();

            // determina o titulo da tela
            this.Text = "Aim Tester";
            // determina largura e altura
            this.Size = new Size(500, 500);
            // determina a posição inicial da tela -> nesse caso centralizada
            this.StartPosition = FormStartPosition.CenterParent;

            btnIniciar = new Button()
            {
                Text = "iniciar",
                Size = new Size(100, 50),
            }; 

             btnIniciar.Click += IniciarJogo;
            //adiciona o botão na tela;

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
            // desabilita o botaão 
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

        private void btnAlvoClick(object sender, EventArgs e)
        {
            stopwatch.Stop();
            btnAlvo.Visible = false;
            MessageBox.Show($"Tempo de reação: {stopwatch.ElapsedMilliseconds}", "ms");
            Task.Delay(500).ContinueWith( _ => IniciarNovaRodada(),
                TaskScheduler.FromCurrentSynchronizationContext());
        }

    }
}
