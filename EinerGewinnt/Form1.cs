﻿#nullable enable

using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace EinerGewinnt;

public partial class Form1 : Form
{
    private readonly Button[,] _spielFeld = new Button[7, 6];
    private readonly Label _animation = new Label();
    private readonly Label _label = new Label();
    private int _x;

    public Form1()
    {
        InitializeComponent();
        Spielfelderzeugen();
    }

    /**
     * @TODO: Animation hinzufügen
     * @TODO: Feuerwerk hinzufügen
     * @TODO: Spielstand speichern (Punktestand)
     */
    
    private void NextMove(object sender, EventArgs e)
    {
        var clickedButton = (Button)sender;
        var columnIndex = int.Parse(clickedButton.Name.Substring(6, 1));

        for (var y = 5; y >= 0; y--)
            if (_spielFeld[columnIndex, y].BackColor == SystemColors.Control ||
                _spielFeld[columnIndex, y].BackColor == Color.White)
            {
                _spielFeld[columnIndex, y].BackColor = _x % 2 == 0 ? Color.Red : Color.Yellow;
                _x++;
                break;
            }
        
        _animation.Location = clickedButton.Location;
        _animation.BackColor = Color.Aqua;
        //_animation.BackColor = _x % 2 == 0 ? Color.Red : Color.Yellow;


        var animationLocation = _animation.Location;
        animationLocation.Y = 180;
        

        try
        {
            for (var i = 0; i < 7; i++)
            for (var j = 0; j < 6; j++)
                if (j < 3)
                    if ((_spielFeld[i, j].BackColor == Color.Red ||
                         _spielFeld[i, j].BackColor == Color.Yellow) &&
                        _spielFeld[i, j].BackColor == _spielFeld[i, j + 1].BackColor &&
                        _spielFeld[i, j].BackColor == _spielFeld[i, j + 2].BackColor &&
                        _spielFeld[i, j].BackColor == _spielFeld[i, j + 3].BackColor)
                        Altf4();
            for (var i = 0; i < 7; i++)
            for (var j = 0; j < 6; j++)
                if (i < 4)
                    if ((_spielFeld[i, j].BackColor == Color.Red ||
                         _spielFeld[i, j].BackColor == Color.Yellow) &&
                        _spielFeld[i, j].BackColor == _spielFeld[i + 1, j].BackColor &&
                        _spielFeld[i, j].BackColor == _spielFeld[i + 2, j].BackColor &&
                        _spielFeld[i, j].BackColor == _spielFeld[i + 3, j].BackColor)
                        Altf4();
            for (var i = 0; i < 7; i++)
            for (var j = 0; j < 6; j++)
                if (i < 4 && j < 3)
                    if ((_spielFeld[i, j].BackColor == Color.Red ||
                         _spielFeld[i, j].BackColor == Color.Yellow) &&
                        _spielFeld[i, j].BackColor == _spielFeld[i + 1, j + 1].BackColor &&
                        _spielFeld[i, j].BackColor == _spielFeld[i + 2, j + 2].BackColor &&
                        _spielFeld[i, j].BackColor == _spielFeld[i + 3, j + 3].BackColor)
                        Altf4();
            for (var i = 0; i < 7; i++)
            for (var j = 0; j < 6; j++)
                if (i < 4 && j >= 3)
                {
                    if ((_spielFeld[i, j].BackColor == Color.Red ||
                         _spielFeld[i, j].BackColor == Color.Yellow) &&
                        _spielFeld[i, j].BackColor == _spielFeld[i + 1, j - 1].BackColor &&
                        _spielFeld[i, j].BackColor == _spielFeld[i + 2, j - 2].BackColor &&
                        _spielFeld[i, j].BackColor == _spielFeld[i + 3, j - 3].BackColor)
                        Altf4();
                }

        }
        catch (IndexOutOfRangeException exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private void Altf4()
    {
        MessageBox.Show((_x % 2 != 0 ? "Rot" : "Gelb") + @" hat gewonnen!");
        for (var i = 0; i < 7; i++)
        for (var j = 0; j < 6; j++)
            _spielFeld[i, j].BackColor = Color.White;
        
    }

    private void Spielfelderzeugen()
    {
        _animation.AutoSize = false;
        _animation.Size = new Size(76, 76);
        _animation.BackColor = SystemColors.Control;
        _animation.Location = new Point(100, 100);
        Controls.Add(_animation);
        
        _label.AutoSize = true;
        _label.Text = @"Spieler 1 ist Rot, Spieler 2 ist Gelb";
        _label.Location = new Point(3*80+200, 8*80+50);
        Controls.Add(_label);
        for (var x = 0; x < 7; x++)
        for (var y = 0; y < 6; y++)
        {
            _spielFeld[x, y] = new Button();
            _spielFeld[x, y].Name = "button" + x + "." + y;
            _spielFeld[x, y].Text = @"_";
            _spielFeld[x, y].Size = new Size(80, 80);
            _spielFeld[x, y].Location = new Point((x + 1) * 80 + 200, (y + 1) * 80 + 100);
            _spielFeld[x, y].AutoSize = false;
            _spielFeld[x, y].ResumeLayout(false);
            _spielFeld[x, y].Click += NextMove;
            Controls.Add(_spielFeld[x, y]);
        }
    }


    private void timer1_Elapsed(object sender, ElapsedEventArgs e)
    {
    }
}