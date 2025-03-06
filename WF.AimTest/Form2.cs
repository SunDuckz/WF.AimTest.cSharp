using System.Diagnostics;

namespace WF.AimTest
{
    public partial class Form2 : Form
    {
        // Local de
        // variaveis
        // propriedades

        // Buttons
        private Button btnVoltar;

        private Button btnIniciar;
        private Button btnAlvo;

        // timer
        private System.Windows.Forms.Timer timer;

        // random
        private Random random;

        // Stopwatch
        private Stopwatch stopwatch;


        // labels
        List<Label> lblList = new List<Label>();

        int lblAtual = 0;
     



        // construtor da tela
        public Form2()
        {
            InitializeComponent();

            // determina o titulo da tela
            this.Text = "Modo Placar";
            // determina largura e altura
            this.Size = new Size(500, 500);
            // determina a posição inicial da tela -> nesse caso centralizada
            this.StartPosition = FormStartPosition.CenterParent;


            for (int i = 0; i < 5; i++)
            {
                lblList.Add(new Label()
                {
                    Name = "lbl" + i + 1,
                    Text = "???",
                    Font = new Font("Arial", 8),
                    
                });
            }
            posicionarLabels();



            btnIniciar = new Button()
            {
                Text = "Iniciar modo Placar",
                Size = new Size(100, 50),
                Top = 0,
            };

            btnVoltar = new Button()
            {
                Text = "Voltar",
                Size = new Size(100, 50),
                Top = ClientSize.Height - 50
               
            };



            //btnVoltar.Click += Close();
            btnIniciar.Click += IniciarJogo;
            //adiciona o botão na tela;

            this.Controls.Add(btnIniciar);
            this.Controls.Add(btnVoltar);
            this.btnAlvo = new Button()
            {
                Size = new Size(100, 100),
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

        private void Voltar(object sender, EventArgs e)
        {
            Close();
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

        private void MostrarBotaoAlvo(object sender, EventArgs e)
        {
            // para o timer
            timer.Stop();

            int x = random.Next(50, this.ClientSize.Width - 100);
            int y = random.Next(50, this.ClientSize.Height - 70);

            btnAlvo.Location = new Point(x, y);

            btnAlvo.Visible = true;
            stopwatch.Restart();
        }


        private void btnAlvoDoubleCLick(object sender, EventArgs e)
        {
            stopwatch.Stop();

            btnAlvo.Visible = false;
            
            Task.Delay(500).ContinueWith(_ => IniciarNovaRodada(),
                TaskScheduler.FromCurrentSynchronizationContext());
        }


        private void btnAlvoClick(object sender, EventArgs e)
        {

            if (btnAlvo.Size.Width >= 20 || btnAlvo.Size.Height >= 20)
            {
                // Aterando tamanho do botão
                btnAlvo.Size = new Size(btnAlvo.Size.Width - 10, btnAlvo.Size.Height - 10);

            }
            
             

                stopwatch.Stop();
                btnAlvo.Visible = false;

            lblList[lblAtual].Text = $"{stopwatch.ElapsedMilliseconds.ToString()} ms";
            lblAtual++;
            MessageBox.Show($"Tempo de reação: {stopwatch.ElapsedMilliseconds} ms", "ms");
            Task.Delay(500).ContinueWith(_ => IniciarNovaRodada(),
                    TaskScheduler.FromCurrentSynchronizationContext());

          if (lblAtual > 4)
            {
                lblAtual = 0;
            }

        }


        private void posicionarLabels()
        {
            int topvalue = 60;
            foreach (Label item in lblList)
            {
                item.Left = 0;
                item.Top = topvalue;
                this.Controls.Add(item);
                topvalue += 20;

            }
        }

    }
}
