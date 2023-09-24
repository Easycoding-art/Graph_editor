namespace Lab3_3sem_UI;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>

    public int x = 1;
    public int y = 1;
    public int d = 60;
    public double text = 0.0;
    public double multiplier = 1.0;
    public int x_center = 1;
    public int y_center = 1;
    public int x_start = 1;
    public int y_start = 1;
    public int move_req = 1;
    public int speed_error = 0;
    public int x_a = 0;
    public int y_a = 0; 
    public int x_b = 0; 
    public int y_b = 0;
    public int x_start_drawning = 0;
    public int y_start_drawning = 0;
    public int x_end_drawning = 0;
    public int y_end_drawning = 0;
    Button button_safe;
    Button button_load;
    Button button_generate_graph;

    private void Form1_Paint(object sender, PaintEventArgs e) {
        Graphics gr = e.Graphics;
        foreach (particle krug in list) {
            int x_cur = krug.X - d/2;
            int y_cur = krug.Y - d/2;
            gr.FillEllipse(Brushes.Brown, x_cur, y_cur, krug.D, krug.D);
        }
    }

    void line_paint(object sender, PaintEventArgs e) {
            Graphics gr = e.Graphics;
            foreach (line l in line_list) {
                    Pen pen = new Pen(Color.Black, 5);
                    double angle = Math.Asin((l.Y_B -l.Y_A)/Math.Sqrt(Math.Pow(l.X_B -l.X_A, 2) + Math.Pow(l.Y_B - l.Y_A, 2)));
                    //gr.DrawArc(pen, l.X_A - l.X_B, l.Y_A - l.Y_B, 100, 200, 90, 180);
                    double radians;
                    double pointer_angle = 30.0;
                    radians = Math.PI * pointer_angle / 180.0 + angle;
                    if (Math.Cos(angle) < 0) {
                            radians = radians + Math.PI/2;
                    }
                    double cos = Math.Cos(radians);
                    double sin = Math.Sin(radians);
                    gr.DrawLine(pen, l.X_A, l.Y_A, l.X_B, l.Y_B);
                    if (l.X_B >= l.X_A) {
                        //first line generates good line exept generated graphs
                        gr.DrawLine(pen, l.X_B, l.Y_B, l.X_B - Convert.ToInt64(d*cos/2), l.Y_B - Convert.ToInt32(d*sin/2));
                        //gr.DrawLine(pen, l.X_B, l.Y_B, l.X_B + Convert.ToInt32(d * sin / 2), l.Y_B + Convert.ToInt32(d * cos / 2));
            }
                    else {
                        gr.DrawLine(pen, l.X_B, l.Y_B, l.X_B - Convert.ToInt32(d*(cos)/2), l.Y_B + Convert.ToInt32(d* (sin +1.5) / 2));
                    }
                    //gr.DrawLine(pen, l.X_B, l.Y_B, l.X_B - Convert.ToInt32(d*cos/2), l.Y_B + Convert.ToInt32(d*sin/2));
                    //gr.DrawLine(pen, l.X_B, l.Y_B
                    //            l.X_B - Convert.ToInt32((l.X_B -l.X_A)/Math.Sqrt(Math.Pow(l.X_B -l.X_A, 2) + Math.Pow(l.Y_B - l.Y_A, 2))*(d/2)), 
                    //            l.Y_B - Convert.ToInt32((l.Y_B -l.Y_A)/Math.Sqrt(Math.Pow(l.X_B -l.X_A, 2) + Math.Pow(l.Y_B - l.Y_A, 2))*(d/2)));
                    //gr.DrawLine(pen, l.X_B, l.Y_B, 
                    //            l.X_B + Convert.ToInt32((l.Y_B - l.Y
                    //            l.Y_B + Convert.ToInt32((l.X_B -l.X_A)/Math.Sqrt(Math.Pow(l.X_B -l.X_A, 2) + Math.Pow(l.Y_B - l.Y_A, 2))*(d/2)));
            }
    }

    void Form1_MouseClick(object sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Left) {
            x = e.Location.X;
            y = e.Location.Y;
            foreach(particle krug in list) {
                if (krug.X - d/2 < x && krug.X + d/2 >= x && krug.Y - d/2 < y && krug.Y + d/2 >= y) { 
                    return;//выйти из функции добавления фигуры
                }
            } 
            this.Paint += new PaintEventHandler(Form1_Paint);
            list.Add(new particle(x, y, d, "None")); //добавляем круг в список
            this.Invalidate(); //перерисовываем форму
        }
    }

    void line_MouseClick(object sender, MouseEventArgs e) {
        int error = 0;
        if (e.Button == MouseButtons.Right && move_req % 3 == 0) {
            x = e.Location.X;
            y = e.Location.Y;
            foreach(particle krug in list) {
                if (krug.X - d/2 < x && krug.X + d/2 >= x && krug.Y - d/2 < y && krug.Y + d/2 >= y) {
                    if (((x_a == 0 && y_a == 0) && (x_b != 0 && y_b !=0)) ||((x_a == 0 && y_a == 0) && (x_b == 0 && y_b ==0))) {
                        x_a = krug.X;
                        y_a = krug.Y;
                        return;
                    }

                    else if ((x_a != 0 && y_a != 0) && (x_b != 0 && y_b !=0)) {
                        x_a = krug.X;
                        y_a = krug.Y;
                        x_b = 0;
                        y_b = 0;
                        return;
                    }

                    else if ((x_a != 0 && y_a != 0) && (x_b == 0 && y_b ==0)) {
                        x_b = krug.X;
                        y_b = krug.Y;
                        this.Paint += new PaintEventHandler(line_paint);
                        line_list.Add(new line(x_a, y_a, x_b, y_b, 0, 0, "None"));
                        //x_b = 0;
                        //y_b = 0;
                        //x_a = 0;
                        //y_a = 0;
                        Settings_connection_create(0, 0, "None");
                        return;
                    }
                }
            }
                /*
                foreach(particle krug in list) {
                    if (x_a == krug.X && y_a == krug.Y) {
                        d1 = krug;
                        return;
                    }

                    else if (x_b == krug.X && y_b == krug.Y) {
                        d2 = krug;
                        return;
                    }
                }
                */
                //this.Paint += new PaintEventHandler(line_paint);
                        //line_list.Add(new line(d1, d2));
                        //x_b = 0;
                        //y_b = 0;
                        //x_a = 0;
                        //y_a = 0;
                        this.Invalidate();
        }
    }

    void delete(object sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Right && move_req % 2 != 0 && move_req % 3 != 0) {
            x = e.Location.X;
            y = e.Location.Y;
            x_a = 0;
            y_a = 0;
            x_b = 0;
            y_b = 0;
            int X1 = 0;
            int Y1 = 0;
            int X2 = 0;
            int Y2 = 0;
            foreach (particle krug in list) {
                if (krug.X - d/2 < x && krug.X + d/2 >= x && krug.Y - d/2 < y && krug.Y + d/2 >= y) {
                    list.Remove(krug);
                    foreach (line l in line_list) {
                        if ((l.X_A == krug.X && l.Y_A == krug.Y) || (l.X_B == krug.X && l.Y_B == krug.Y)) {
                            X1 = l.X_A;
                            Y1 = l.Y_A;
                            X2 = l.X_B;
                            Y2 = l.Y_B;
                            delete_list.Add(l);
                        }
                    }
            
                    foreach (line l in line_list) {
                        if ((l.X_A == X2 && l.Y_A == Y2) && (l.X_B == X1 && l.Y_B == Y1)) {
                            delete_list.Add(l);
                        }
                    }
                    foreach(line i in delete_list) {
                        line_list.Remove(i);
                    }
                    delete_list.Clear();
                    this.Invalidate();
                    break;
                }
            }
        }
    }

    void test_move(object sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Right && move_req == 2) {
            x = e.Location.X;
            y = e.Location.Y;
            foreach (particle krug in list) {
                if (krug.X - d/2 < x && krug.X + d/2 >= x && krug.Y - d/2 < y && krug.Y + d/2 >= y) {
                    x_start = krug.X;
                    y_start = krug.Y;
                    break;
                }
                else if(krug.X.Equals(x_start)  && krug.Y.Equals(y_start)) {
                krug.X = x;
                krug.Y = y;
                    foreach (line l in line_list) {
                        if (l.X_A == x_start && l.Y_A == y_start) {
                            line_list[line_list.IndexOf(l)].X_A = x;
                            line_list[line_list.IndexOf(l)].Y_A = y;
                            //break;
                        }
                        else if (l.X_B == x_start && l.Y_B == y_start) {
                            line_list[line_list.IndexOf(l)].X_B = x;
                            line_list[line_list.IndexOf(l)].Y_B = y;
                            //break;
                        }
                    }
                    foreach (line l in line_list) {
                        if (l.X_B == x_start && l.Y_B == y_start) {
                            line_list[line_list.IndexOf(l)].X_B = x;
                            line_list[line_list.IndexOf(l)].Y_B = y;
                            //break;
                        }
                        else if (l.X_A == x_start && l.Y_A == y_start) {
                            line_list[line_list.IndexOf(l)].X_A = x;
                            line_list[line_list.IndexOf(l)].Y_A = y;
                            //break;
                        }
                    }
                    this.Invalidate();
                    break;
                }
            }
        }
    }

    Button up;
    Button down;
    TextBox size;
    TextBox panel;
    Button del_or_move;

    private void Move_Click(object sender, System.EventArgs e) {
        if(move_req == 4) {
            move_req = 1;
        }
        else {
            move_req++;
        }
        change_move(move_req);
    }

    private void change_move(int mode) {
        if (move_req == 1) {
            del_or_move.Text = "Режим удаления";
            del_or_move.BackColor = Color.Silver;
        }

        else if (move_req == 2) {
           del_or_move.Text = "Режим перемещения";
           del_or_move.BackColor = Color.Sienna; 
        }

        else if (move_req == 3) {
           del_or_move.Text = "Режим соединения";
           del_or_move.BackColor = Color.Navy;
        }

        else if (move_req == 4) {
           del_or_move.Text = "Кратчайший путь";
           del_or_move.BackColor = Color.Chocolate;
        }
    }

    private void print(double count) {
        size.Text = count.ToString();
    }
    private void Up_Click(object sender, System.EventArgs e) {
        d += 15;
        multiplier += 0.25;
        print(multiplier);
        foreach (particle krug in list) {
            krug.D = d;
            this.Invalidate();
        }
    }

    private void Down_Click(object sender, System.EventArgs e) {
        if (d > 15) {
            d -= 15;
            multiplier -= 0.25;
            print(multiplier);
        
            foreach (particle krug in list) {
                krug.D = d;
                this.Invalidate();
            }
        }
    }

    void project_name_change(string project_name) {
        this.Text = project_name;
    }
    
    private void InitializeComponent() {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 500);
        this.AutoSize = true;
        project_name_change("Новый проект");
        this.BackColor = System.Drawing.Color.GhostWhite;

        button_safe = new Button();
        button_safe.Location = new Point(710, 100);
        button_safe.Text = "SAVE";
        button_safe.Size = new Size(80,56);
        button_safe.BackColor = Color.HotPink;
        button_safe.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
        this.Controls.Add(button_safe);
        button_safe.Click += new EventHandler(safe_Click);

        button_generate_graph = new Button();
        button_generate_graph.Location = new Point(710, 180);
        button_generate_graph.Text = "GENERATE GRAPH";
        button_generate_graph.Size = new Size(90,56);
        button_generate_graph.BackColor = Color.Teal;
        button_generate_graph.Anchor = (AnchorStyles.Right);
        this.Controls.Add(button_generate_graph);
        button_generate_graph.Click += new EventHandler(generate_Click);
        
        button_load = new Button();
        button_load.Location = new Point(710, 260);
        button_load.Text = "LOAD";
        button_load.Size = new Size(80,56);
        button_load.BackColor = Color.Indigo;
        button_load.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
        this.Controls.Add(button_load);

        button_load.Click += new EventHandler(load_Click);
        
        this.MouseClick += new MouseEventHandler(Form1_MouseClick);
        this.MouseClick += new MouseEventHandler(line_MouseClick);
        this.MouseClick += new MouseEventHandler(delete);
        this.MouseDoubleClick += new MouseEventHandler(set);
        this.MouseClick += new MouseEventHandler(test_move);
        this.MouseClick += new MouseEventHandler(transport_choice_Click);

        size = new TextBox();
        size.Enabled = false;
        size.Location = new Point(35, 10);
        size.Size = new Size(40,30);
        size.BackColor = Color.OrangeRed;
        size.ForeColor = Color.OliveDrab;
        this.Controls.Add(size);

        panel = new TextBox();
        panel.Text = "X";
        panel.Enabled = false;
        panel.Location = new Point(20, 10);
        panel.Size = new Size(20,10);
        panel.BackColor = Color.OrangeRed;
        panel.ForeColor = Color.OliveDrab;
        this.Controls.Add(panel);

        up = new Button();
        up.Location = new Point(50, 40);
        up.Text = ">";
        up.Size = new Size(20,30);
        up.BackColor = Color.DarkSalmon;
        this.Controls.Add(up);
        up.Click += new EventHandler(Up_Click);

        down = new Button();
        down.Location = new Point(20, 40);
        down.Text = "<";
        down.Size = new Size(20,30);
        down.BackColor = Color.DarkSalmon;
        this.Controls.Add(down);
        down.Click += new EventHandler(Down_Click);

        del_or_move = new Button();
        del_or_move.Location = new Point(80, 10);
        del_or_move.Size = new Size(200,30);
        this.Controls.Add(del_or_move);
        del_or_move.Click += new EventHandler(Move_Click);
        
        change_move(1);
        
        print(multiplier);
    }
    #endregion
}
