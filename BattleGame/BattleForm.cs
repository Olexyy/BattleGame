using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleGame
{
    public partial class BattleForm : Form
    {
        private BattleCore battleCore;
        public BattleForm()
        {
            this.InitializeComponent();
            this.battleCore = new BattleCore();
            this.battleCore.Players.Add(new Player("Player_1"));
            this.PrintArmy();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.PrintArmy();
        }
        private void PrintArmy()
        {
            this.battleCore.MakeArmy(battleCore.Players.First());
            this.listViewUnits.Items.Clear();
            this.battleCore.ShowArmy(this.battleCore.Players.First()).ToList().ForEach(i => this.listViewUnits.Items.Add(new ListViewItem(i)));
        }
    }
}
