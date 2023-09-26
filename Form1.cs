namespace Lab3_3sem_UI;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

public partial class Form1:Form {

        public Form1()
        {
            InitializeComponent();
        }

        //[DllImport("TestLib.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern double Add(double a, double b);

        List<particle> list = new List<particle>(); //список кругов
        List<line> line_list = new List<line>(); //список кругов
        List<line> delete_list = new List<line>();
        List<particle> list_new;
        List<line> line_list_new;

        Form settings;
        Button settings_close;
        Form settings_lines;
        Button settings_lines_close;
        TextBox text_value;
        TextBox length_value;
        TextBox length_text;
        TextBox length_fault;
        TextBox cost_fault;
        TextBox cost_value;
        TextBox cost_text;
        TextBox transport_value;
        TextBox transport_choice_text;
        Form transport_choice;
        Button problem_close;
        TextBox transport_choice_name_value;
    private void Settings_form_close(object sender, System.EventArgs e) {
        x = x_center;
        y = y_center;
        foreach (particle krug in list) {
                if (krug.X == x && krug.Y == y) {
                    krug.Name = text_value.Text;
                    break;
                }
        }
        this.Invalidate();
        settings.Dispose();
    }

    private void Settings_connection_close(object sender, System.EventArgs e) {
  
        if (int.Parse(length_value.Text) <= 0 || int.Parse(cost_value.Text) <= 0){
            if (int.Parse(length_value.Text) <= 0) {
                length_fault.Visible = true;
            }
            else {
                length_fault.Visible = false;
            }

            if (int.Parse(cost_value.Text) <= 0) {
                cost_fault.Visible = true;
            }
            else {
                cost_fault.Visible = false;
            }
        }
        else {  
            line_list[line_list.Count - 1].Transport = transport_value.Text;
            line_list[line_list.Count - 1].Length = int.Parse(length_value.Text);
            line_list[line_list.Count - 1].Cost = int.Parse(cost_value.Text);
            this.Invalidate();
            settings_lines.Dispose();
        }
    }
    void Settings_form_create(string name) {
        if (settings != null) {
            settings.Dispose();
        }
        settings=new Form();
        settings.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        settings.ClientSize = new System.Drawing.Size(400, 200);
        settings.Text = "Настройка точки";
        settings.BackColor = System.Drawing.Color.DarkGoldenrod;
        settings.MaximumSize = new System.Drawing.Size(400, 200);
        settings.MinimumSize = new System.Drawing.Size(400, 200);
        settings.MaximizeBox = false;
        settings.Show();

        settings_close = new Button();
        settings_close.Text = "Continue";
        settings_close.Location = new Point(150, 80);
        settings_close.Size = new Size(80,56);
        settings_close.BackColor = Color.BurlyWood;
        settings.Controls.Add(settings_close);

        text_value = new TextBox();
        text_value.Location = new Point(95, 10);
        text_value.Size = new Size(195,90);
        text_value.Text = name;
        text_value.BackColor = Color.OrangeRed;
        text_value.ForeColor = Color.OliveDrab;
        settings.Controls.Add(text_value);

        TextBox text_text = new TextBox();
        text_text.Text = "Название";
        text_text.Enabled = false;
        text_text.Location = new Point(95, 35);
        text_text.Size = new Size(195,90);
        text_text.BackColor = Color.GreenYellow;
        settings.Controls.Add(text_text);

        settings_close.Click += new EventHandler(Settings_form_close);
    }
    void set(object sender, MouseEventArgs e) {
                x = e.Location.X;
                y = e.Location.Y;
                foreach (particle krug in list) {
                    if (krug.X - d/2 < x && krug.X + d/2 >= x && krug.Y - d/2 < y && krug.Y + d/2 >= y) {
                        Settings_form_create(krug.Name);
                        x_center = krug.X;
                        y_center = krug.Y;
                        break;
                    }
                }
    }

void Settings_connection_create(int length, int cost, string transport) {
        if (settings != null) {
            settings_lines.Dispose();
        }
        settings_lines=new Form();
        settings_lines.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        settings_lines.ClientSize = new System.Drawing.Size(400, 200);
        settings_lines.Text = "Настройка пути";
        settings_lines.BackColor = System.Drawing.Color.DarkGoldenrod;
        settings_lines.MaximumSize = new System.Drawing.Size(400, 250);
        settings_lines.MinimumSize = new System.Drawing.Size(400, 250);
        settings_lines.MaximizeBox = false;
        settings_lines.Show();

        settings_lines_close = new Button();
        settings_lines_close.Text = "Continue";
        settings_lines_close.Location = new Point(150, 140);
        settings_lines_close.Size = new Size(80,56);
        settings_lines_close.BackColor = Color.BurlyWood;
        settings_lines.Controls.Add(settings_lines_close);

        transport_value = new TextBox();
        transport_value.Location = new Point(35, 10);
        transport_value.Size = new Size(85,90);
        transport_value.Text = transport;
        transport_value.BackColor = Color.OrangeRed;
        transport_value.ForeColor = Color.OliveDrab;
        settings_lines.Controls.Add(transport_value);

        TextBox transport_text = new TextBox();
        transport_text.Text = "Транспорт";
        transport_text.Enabled = false;
        transport_text.Location = new Point(35, 35);
        transport_text.Size = new Size(85,90);
        transport_text.BackColor = Color.GreenYellow;
        settings_lines.Controls.Add(transport_text);

        length_value = new TextBox();
        length_value.Location = new Point(125, 10);
        length_value.Size = new Size(105,90);
        length_value.Text = length.ToString();
        length_value.BackColor = Color.OrangeRed;
        length_value.ForeColor = Color.OliveDrab;
        settings_lines.Controls.Add(length_value);

        length_fault = new TextBox();
        length_fault.Location = new Point(15, 75);
        length_fault.Size = new Size(320,90);
        length_fault.Enabled = false;
        length_fault.Visible = false;
        length_fault.Text = "Длина не может быть отрицательной или 0!";
        length_fault.BackColor = Color.Red;
        settings_lines.Controls.Add(length_fault);

        TextBox length_text = new TextBox();
        length_text.Text = "Длина";
        length_text.Enabled = false;
        length_text.Location = new Point(125, 35);
        length_text.Size = new Size(105,90);
        length_text.BackColor = Color.GreenYellow;
        settings_lines.Controls.Add(length_text);

        cost_value = new TextBox();
        cost_value.Location = new Point(240, 10);
        cost_value.Size = new Size(105,90);
        cost_value.Text = cost.ToString();
        cost_value.BackColor = Color.OrangeRed;
        cost_value.ForeColor = Color.OliveDrab;
        settings_lines.Controls.Add(cost_value);

        TextBox cost_text = new TextBox();
        cost_text.Text = "Стоимость";
        cost_text.Enabled = false;
        cost_text.Location = new Point(240, 35);
        cost_text.Size = new Size(105,90);
        cost_text.BackColor = Color.GreenYellow;
        settings_lines.Controls.Add(cost_text);

        cost_fault = new TextBox();
        cost_fault.Location = new Point(15, 110);
        cost_fault.Size = new Size(355,90);
        cost_fault.Enabled = false;
        cost_fault.Visible = false;
        cost_fault.Text = "Стоимость не может быть отрицательной или 0!";
        cost_fault.BackColor = Color.Red;
        settings_lines.Controls.Add(cost_fault);

        settings_lines_close.Click += new EventHandler(Settings_connection_close);
    }

        Form load;
        Form safe;
        Button loader;
        Button saver;
        TextBox saving_project_name_value;
        TextBox loading_project_name_value;
        TextBox loading_error;
        TextBox saving_error;
        Form problem;

        private void save_continue_Click(object sender, System.EventArgs e) {
            int i = 0;
            foreach (particle krug in list) {
                i += 1;
            }
            if (i > 0) {
                string project_name = saving_project_name_value.Text;
                Save_project(project_name);
                safe.Dispose(); 
            }
            else {
                saving_error.Visible = true;
            }
        }

        private void load_continue_Click(object sender, System.EventArgs e) {
            string project_name = loading_project_name_value.Text;
            if(!File.Exists("Project_storage/" + project_name + ".txt") ) {
                loading_error.Visible = true;
            }
            else {
                Load_project(project_name);
                load.Dispose();
            }
        }
        private void safe_Click(object sender,EventArgs e) {
            if (safe != null) {
                safe.Dispose();
            }
            safe=new Form();
            safe.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            safe.ClientSize = new System.Drawing.Size(400, 130);
            safe.Text = "Сохранение";
            safe.MaximumSize = new System.Drawing.Size(400, 180);
            safe.MinimumSize = new System.Drawing.Size(400, 180);
            safe.MaximizeBox = false;
            safe.BackColor = System.Drawing.Color.DarkGoldenrod;
            safe.Show();

            saver = new Button();
            saver.Text = "Continue";
            saver.Location = new Point(150, 70);
            saver.Size = new Size(80,56);
            saver.BackColor = Color.BurlyWood;
            safe.Controls.Add(saver);

            saving_project_name_value = new TextBox();
            saving_project_name_value.Location = new Point(35, 10);
            saving_project_name_value.Size = new Size(325, 90);
            saving_project_name_value.Text = "Имя_проекта";
            saving_project_name_value.BackColor = Color.OrangeRed;
            saving_project_name_value.ForeColor = Color.OliveDrab;
            safe.Controls.Add(saving_project_name_value);

            saving_error = new TextBox();
            saving_error.Location = new Point(90, 40);
            saving_error.Size = new Size(225,90);
            saving_error.Text = "Пустой проект!";
            saving_error.BackColor = Color.Red;
            saving_error.Enabled = false;
            saving_error.Visible = false;
            safe.Controls.Add(saving_error);

            saver.Click += new EventHandler(save_continue_Click);
        }

        private void load_Click(object sender,EventArgs e) {
            if (load != null) {
                load.Dispose();
            }
            load=new Form();
            load.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            load.ClientSize = new System.Drawing.Size(400, 130);
            load.Text = "Загрузка";
            load.MaximumSize = new System.Drawing.Size(400, 180);
            load.MinimumSize = new System.Drawing.Size(400, 180);
            load.MaximizeBox = false;
            load.BackColor = System.Drawing.Color.DarkGoldenrod;
            load.Show();

            loader = new Button();
            loader.Text = "Continue";
            loader.Location = new Point(150, 70);
            loader.Size = new Size(80,56);
            loader.BackColor = Color.BurlyWood;
            load.Controls.Add(loader);

            loading_project_name_value = new TextBox();
            loading_project_name_value.Location = new Point(35, 10);
            loading_project_name_value.Size = new Size(325,90);
            loading_project_name_value.Text = "Имя_проекта";
            loading_project_name_value.BackColor = Color.OrangeRed;
            loading_project_name_value.ForeColor = Color.OliveDrab;
            load.Controls.Add(loading_project_name_value);

            loading_error = new TextBox();
            loading_error.Location = new Point(90, 40);
            loading_error.Size = new Size(225,90);
            loading_error.Text = "Несуществующий проект!";
            loading_error.BackColor = Color.Red;
            loading_error.Enabled = false;
            loading_error.Visible = false;
            load.Controls.Add(loading_error);

            loader.Click += new EventHandler(load_continue_Click);
        }

    void Save_project(string project_name) {
        project_name_change(project_name);
        string collect_data = "";
        foreach (particle krug in list) {
            collect_data = collect_data + krug.X.ToString() + " " + krug.Y.ToString() + " " + krug.D.ToString() + " " + krug.Name + " " + multiplier.ToString() + " #";
        }
        foreach (line l in line_list) {
            collect_data = collect_data + "/" + " " + l.X_A.ToString() + " " + l.Y_A.ToString() + " " + l.X_B.ToString() + " " + l.Y_B.ToString() + " " + l.Length.ToString() + " " + l.Cost.ToString() + " " + l.Transport + " #";
        }
        collect_data = collect_data.Remove(collect_data.Length - 2);
        collect_data = collect_data.Replace("#", "#" + System.Environment.NewLine);
        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Project_storage"));
        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "Project_storage", project_name + ".txt"), collect_data);
    }

    void Load_project(string project_name) {
        project_name_change(project_name);
        string[] readText = File.ReadAllLines("Project_storage/" + project_name + ".txt");
        int i = 0;
        int X;
        int Y;
        int x_a;
        int y_a;
        int x_b;
        int y_b;
        string transport;
        int length;
        int cost;
        string name;
        List<double> c = new List<double>();
        List<double> v = new List<double>();
        List<double> a = new List<double>();
        foreach (particle krug in list.ToList()) {
            list.Remove(krug);
        }
        foreach (line l in line_list.ToList()) {
            line_list.Remove(l);
        }
        foreach (string s in readText) {
            string[] data = s.Split(new char[] { ' ' });
            if (data[0] == "/") {
                x_a = Convert.ToInt32(data[1]);
                y_a = Convert.ToInt32(data[2]);
                x_b = Convert.ToInt32(data[3]);
                y_b = Convert.ToInt32(data[4]);
                length = Convert.ToInt32(data[5]);
                cost = Convert.ToInt32(data[6]);
                transport = data[7];
                this.Paint += new PaintEventHandler(line_paint);
                line_list.Add(new line(x_a, y_a, x_b, y_b, length, cost, transport));
            }
            else {
                X = Convert.ToInt32(data[0]);
                Y = Convert.ToInt32(data[1]);
                d = Convert.ToInt32(data[2]); 
                name = data[3];
                multiplier = Convert.ToDouble(data[4]);
                this.Paint += new PaintEventHandler(Form1_Paint);
                list.Add(new particle(X, Y, d, name));
                i+=1; 
            }
        }
        size.Text = multiplier.ToString();
        this.Invalidate();    
    }
    string blocked;
    void way_paint(object sender, PaintEventArgs e) {
        list_new = list; //список кругов
        line_list_new = line_list; //список кругов
        
        // transport
        List<int> Block_list = new List<int>();
        foreach (line l in line_list_new) {
            if (blocked.Contains(l.Transport)) {
                Block_list.Add(line_list_new.IndexOf(l));
            }
        }
        foreach(int i in Block_list) {
            line_list_new.RemoveAt(i);
        }
        Block_list.Clear();
        Process p = new Process();
        p.StartInfo.UseShellExecute = false;

        // Перехватываем вывод
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.CreateNoWindow = true;
        // Запускаемое приложение
        p.StartInfo.FileName = "Solution.exe";
        //p.StartInfo.FileName = "example.exe";

        // Передаем необходимые аргументы
        // p.Arguments = "example.txt";
        p.Start();
        p.StandardInput.WriteLine(line_list_new.Count.ToString());
        foreach (line l in line_list_new) {
            p.StandardInput.WriteLine(l.X_A.ToString()); //передача данных в консоль
            p.StandardInput.WriteLine(l.Y_A.ToString());
            p.StandardInput.WriteLine(l.X_B.ToString());
            p.StandardInput.WriteLine(l.Y_B.ToString());
            p.StandardInput.WriteLine(l.Length.ToString());
            p.StandardInput.WriteLine(l.Cost.ToString());
        }
        p.StandardInput.WriteLine(x_start_drawning.ToString());
        p.StandardInput.WriteLine(y_start_drawning.ToString());
        p.StandardInput.WriteLine(x_end_drawning.ToString());
        p.StandardInput.WriteLine(y_end_drawning.ToString());
        // Результат работы консольного приложения
        // Дождаться завершения запущенного приложения
        string output = p.StandardOutput.ReadToEnd(); 
        p.WaitForExit();

        Graphics gr = e.Graphics;
        foreach (particle krug in list_new) {
            int x_cur = krug.X - d/2;
            int y_cur = krug.Y - d/2;
            gr.FillEllipse(Brushes.Brown, x_cur, y_cur, krug.D, krug.D);
        }

        string[] readText = output.Split('\n');
        Array.Resize(ref readText, readText.Length - 1);
        foreach (string s in readText) {
            string[] data = s.Split(new char[] { ' ' });
            Pen pen = new Pen(Color.Red, 5);
            //gr.DrawArc(pen, l.X_A - l.X_B, l.Y_A - l.Y_B, 100, 200, 90, 180);
            double angle = Math.Asin((Convert.ToInt32(data[3]) -Convert.ToInt32(data[1]))/Math.Sqrt(Math.Pow(Convert.ToInt32(data[2]) - Convert.ToInt32(data[0]), 2) + Math.Pow(Convert.ToInt32(data[3]) - Convert.ToInt32(data[1]), 2)));
            //gr.DrawArc(pen, l.X_A - l.X_B, l.Y_A - l.Y_B, 100, 200, 90, 180);
            double radians;
            radians = Math.PI * 30.0 / 180.0 + angle;
            if (Math.Cos(angle) < 0) {
                    radians = radians + Math.PI/2;
            }
            double cos = Math.Round(Math.Cos(radians) - (Convert.ToInt32(data[3]) -Convert.ToInt32(data[1]))/Math.Sqrt(Math.Pow(Convert.ToInt32(data[2]) -Convert.ToInt32(data[0]), 2) + Math.Pow(Convert.ToInt32(data[3]) - Convert.ToInt32(data[1]), 2)), 2);
            double sin = Math.Round(Math.Sin(radians) - (Convert.ToInt32(data[2]) -Convert.ToInt32(data[0]))/Math.Sqrt(Math.Pow(Convert.ToInt32(data[2]) -Convert.ToInt32(data[0]), 2) + Math.Pow(Convert.ToInt32(data[3]) - Convert.ToInt32(data[1]), 2)), 2);
            gr.DrawLine(pen, Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), Convert.ToInt32(data[2]), Convert.ToInt32(data[3]));
            if (Convert.ToInt32(data[2]) >= Convert.ToInt32(data[0])) {
                gr.DrawLine(pen, Convert.ToInt32(data[2]), Convert.ToInt32(data[3]), Convert.ToInt32(data[2]) - Convert.ToInt32(d*cos), Convert.ToInt32(data[3]) - Convert.ToInt32(d*sin));
                gr.DrawLine(pen, Convert.ToInt32(data[2]), Convert.ToInt32(data[3]), Convert.ToInt32(data[2]) + Convert.ToInt32(d*cos), Convert.ToInt32(data[3]) - Convert.ToInt32(d*sin));
            }
            else {
                gr.DrawLine(pen, Convert.ToInt32(data[2]), Convert.ToInt32(data[3]), Convert.ToInt32(data[2]) - Convert.ToInt32(d*cos), Convert.ToInt32(data[3]) - Convert.ToInt32(d*sin));
                gr.DrawLine(pen, Convert.ToInt32(data[2]), Convert.ToInt32(data[3]), Convert.ToInt32(data[2]) + Convert.ToInt32(d*cos), Convert.ToInt32(data[3]) - Convert.ToInt32(d*sin));
            }
        }
    }

    private void problem_Click(object sender, EventArgs e) {
            if (problem != null) {
                problem.Dispose();
            }
            problem=new Form();
            problem.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            problem.ClientSize = new System.Drawing.Size(800, 500);
            problem.Text = "Результат";
            problem.BackColor = System.Drawing.Color.Gray;
            problem.Paint += new PaintEventHandler(way_paint);
            problem.Show();
            blocked = transport_choice_name_value.Text;
            transport_choice.Dispose();
            problem.Invalidate();
    }

    private void transport_choice_Click(object sender, MouseEventArgs e) {
            if (problem != null) {
                transport_choice.Dispose();
            }
            if (e.Button == MouseButtons.Right && move_req % 4 == 0) {
                x = e.Location.X;
                y = e.Location.Y;
                if ((x_start_drawning == 0 && y_start_drawning == 0 && x_end_drawning == 0 && y_end_drawning == 0)
                    || (x_start_drawning != 0 && y_start_drawning != 0 && x_end_drawning != 0 && y_end_drawning != 0)) {
                    foreach (particle krug in list) {
                        if (krug.X - d/2 < x && krug.X + d/2 >= x && krug.Y - d/2 < y && krug.Y + d/2 >= y) {
                            x_start_drawning = krug.X;
                            y_start_drawning = krug.Y;
                            x_end_drawning = 0;
                            y_end_drawning = 0;
                        }
                    }
                }
                else {
                    foreach (particle krug in list) {
                        if (krug.X - d/2 < x && krug.X + d/2 >= x && krug.Y - d/2 < y && krug.Y + d/2 >= y) {
                            x_end_drawning = krug.X;
                            y_end_drawning = krug.Y;
                        }
                    }
                    transport_choice=new Form();
                    transport_choice.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                    transport_choice.ClientSize = new System.Drawing.Size(400, 200);
                    transport_choice.Text = "Запрет транспорта";
                    transport_choice.MaximumSize = new System.Drawing.Size(400, 200);
                    transport_choice.MinimumSize = new System.Drawing.Size(400, 200);
                    transport_choice.MaximizeBox = false;
                    transport_choice.BackColor = System.Drawing.Color.DarkGoldenrod;
                    transport_choice.Show();
                    
                    problem_close = new Button();
                    problem_close.Text = "Continue";
                    problem_close.Location = new Point(150, 80);
                    problem_close.Size = new Size(80,56);
                    problem_close.BackColor = Color.BurlyWood;
                    transport_choice.Controls.Add(problem_close);

                    transport_choice_name_value = new TextBox();
                    transport_choice_name_value.Location = new Point(95, 10);
                    transport_choice_name_value.Size = new Size(195,90);
                    transport_choice_name_value.Text = "Transport";
                    transport_choice_name_value.BackColor = Color.OrangeRed;
                    transport_choice_name_value.ForeColor = Color.OliveDrab;
                    transport_choice.Controls.Add(transport_choice_name_value);

                    TextBox transport_choice_text = new TextBox();
                    transport_choice_text.Text = "Название";
                    transport_choice_text.Enabled = false;
                    transport_choice_text.Location = new Point(95, 35);
                    transport_choice_text.Size = new Size(195,90);
                    transport_choice_text.BackColor = Color.GreenYellow;
                    transport_choice.Controls.Add(transport_choice_text);

                    problem_close.Click += new EventHandler(problem_Click);
                }
            }
    }
    
    private void generate_Click(object sender,EventArgs e) {
        Process p = new Process();
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.Arguments = "graph_generator.py";
        // Перехватываем вывод
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.CreateNoWindow = true;
        // Запускаемое приложение
        p.StartInfo.FileName = "python";
        //p.StartInfo.FileName = "example.exe";

        // Передаем необходимые аргументы
        // p.Arguments = "example.txt";
        if(!(Directory.Exists("Project_storage"))) {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Project_storage"));
        }
        p.Start();
        string[] file_names = Directory.GetFiles("Project_storage");
        // Результат работы консольного приложения
        p.StandardInput.WriteLine("Project_storage/generated_graph" + Convert.ToString(file_names.Length));
        // Дождаться завершения запущенного приложения
        p.WaitForExit();
        Load_project("generated_graph" + Convert.ToString(file_names.Length));
    }
}



class particle
{
    public int X { set; get; }
    public int Y { set; get; }
    public int D { set; get; }
    public string Name { set; get; }
    public particle(int x, int y, int d, string name)
    {
        this.X = x;
        this.Y = y;
        this.D = d;
        this.Name = name;
    }
}

class line
{
    public int X_A { set; get; }
    public int Y_A { set; get; }
    public int X_B { set; get; }
    public int Y_B { set; get; }
    public int Length { set; get; }
    public int Cost { set; get; }
    public string Transport { set; get; }

    public line(int x_1, int y_1, int x_2, int y_2, int length, int cost, string transport)
    {
        this.X_A = x_1;
        this.X_B = x_2;
        this.Y_A = y_1;
        this.Y_B = y_2;
        this.Length = length;
        this.Cost = cost;
        this.Transport = transport;
    }
}
