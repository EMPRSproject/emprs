﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace EMPRS
{
    public partial class mainView : Form
    {
        Patient curPatient = new Patient();
        public mainView()
        {
            InitializeComponent();

            //default not authorized
            notAuthorizedToolStripMenuItem.Checked = true;

            //initialize labs text boxes empty
            //-----lol surely there is a better way to do this
            ca2MaskTxtBox.Clear();
            tPMaskTxtBox.Clear();
            aSTMaskTxtBox.Clear();
            lDHMastTxtBox.Clear();
            bilMastTxtBox.Clear();
            PO43MaskTxtBox.Clear();
            albMaskTxtBox.Clear();
            aLTMaskTxtBox.Clear();
            aLPMaskTxtBox.Clear();
            iNRMaskTxtBox.Clear();
            pTMaskTxtBox.Clear();
            pTTMaskTxtBox.Clear();
            naMaskTxtBox.Clear();
            clMaskTxtBox.Clear();
            bUNMaskTxtBox.Clear();
            kMaskTxtBox.Clear();
            HCO3MaskTxtBox.Clear();
            creatMaskTxtBox.Clear();
            glucoseMaskTxtBox.Clear();
            rBCMaskTxtBox.Clear();
            wBCMaskTxtBox.Clear();
            hCTMaskTxtBox.Clear();
            hgbMaskTxtBox.Clear();
            pLTMaskTxtBox.Clear();

           // loadData();

            if (global.isAdmin == false)
            {
                ordTabs.TabPages.Remove(tabPage3); //  
            }
            else
            {
                logInAsMaskTxtBox.Text = "Admin";
                enableAdminView(tabPage4); // Notes -> Nursing Orders
                enableAdminView(assessmentTab); // Assessment Data -> Assessment
                enableAdminView(vitalSignTab); // Assessment Data -> Vital Signs
                enableAdminView(labsTab); // Labs -> Labs
                groupBox45.Enabled = true; // Header
            }
        }
        
        public void loadData()
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=C:\\SQLite\\EMPrs.db;Version=3;");
            m_dbConnection.Open();
            string sql = "select nameF, nameL from Patients";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            using (SQLiteDataReader rdr = command.ExecuteReader())
            {
                while (rdr.Read())
                {
                    patientDropDown.Items.Add(Convert.ToString(rdr["nameF"]) + " " + Convert.ToString(rdr["nameL"]));
                }
                rdr.Close();
            }
            m_dbConnection.Close();
        }
        public void loadPatient(string patientName)
        {
            var names = patientName.Split(' ');
            string firstName = names[0];
            string lastName = names[1];
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=C:\\SQLite\\EMPrs.db;Version=3;");
            m_dbConnection.Open();
            string sql = "select * from Patients where nameF == \"" + firstName + "\" And nameL == \"" + lastName + "\"";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            using (SQLiteDataReader rdr = command.ExecuteReader())
            {
                while (rdr.Read())
                {
                    //Patient Data
                    curPatient.nameF = Convert.ToString(rdr["nameF"]);
                    curPatient.nameL = Convert.ToString(rdr["nameL"]);
                    curPatient.patientID = Convert.ToInt32(rdr["patientID"]);
                    curPatient.age = Convert.ToInt32(rdr["age"]);
                    curPatient.DOB = Convert.ToDateTime(rdr["DOB"]);
                    curPatient.sex = (Patient.MF)Convert.ToInt32(rdr["sex"]);
                    curPatient.height = Convert.ToInt32(rdr["height"]);
                    curPatient.weight = Convert.ToInt32(rdr["weight"]);
                    curPatient.allergies = Convert.ToString(rdr["allergies"]);
                    curPatient.infections = Convert.ToString(rdr["infections"]);
                }
                rdr.Close();
            }

            sql = "select * from Labs where patientID == " + curPatient.patientID;
            command = new SQLiteCommand(sql, m_dbConnection);
            using (SQLiteDataReader rdr = command.ExecuteReader())
            {
                while (rdr.Read())
                {
                    //Labs
                    curPatient.labDate = Convert.ToDateTime(rdr["labDate"]);
                    curPatient.sodium = Convert.ToSingle(rdr["sodium"]);
                    curPatient.potassium = Convert.ToSingle(rdr["potassium"]);
                    curPatient.chloride = Convert.ToSingle(rdr["chloride"]);
                    curPatient.HCO3 = Convert.ToSingle(rdr["HCO3"]);
                    curPatient.BUN = Convert.ToSingle(rdr["BUN"]);
                    curPatient.creatinine = Convert.ToSingle(rdr["creatinine"]);
                    curPatient.glucose = Convert.ToSingle(rdr["glucose"]);
                    curPatient.calcium = Convert.ToSingle(rdr["calcium"]);
                    curPatient.magnesium = Convert.ToSingle(rdr["magnesium"]);
                    curPatient.phosphate = Convert.ToSingle(rdr["phosphate"]);
                    curPatient.protein = Convert.ToSingle(rdr["protein"]);
                    curPatient.albumin = Convert.ToSingle(rdr["albumin"]);
                    curPatient.AST = Convert.ToSingle(rdr["AST"]);
                    curPatient.ALT = Convert.ToSingle(rdr["ALT"]);
                    curPatient.LDH = Convert.ToSingle(rdr["LDH"]);
                    curPatient.ALP = Convert.ToSingle(rdr["ALP"]);
                    curPatient.bilirubin = Convert.ToSingle(rdr["bilirubin"]);
                    curPatient.HCT = Convert.ToSingle(rdr["HCT"]);
                    curPatient.RBC = Convert.ToSingle(rdr["RBC"]);
                    curPatient.Hgb = Convert.ToSingle(rdr["HGB"]);
                    curPatient.WBC = Convert.ToSingle(rdr["WBC"]);
                    curPatient.PLT = Convert.ToSingle(rdr["PLT"]);
                    curPatient.PT = Convert.ToSingle(rdr["PT"]);
                    curPatient.PTT = Convert.ToSingle(rdr["PTT"]);
                    curPatient.INR = Convert.ToSingle(rdr["INR"]);
                    curPatient.myoglobin = Convert.ToSingle(rdr["myoglobin"]);
                    curPatient.cTnI = Convert.ToSingle(rdr["cTnI"]);
                    curPatient.cTnT = Convert.ToSingle(rdr["cTnT"]);
                    curPatient.CPK_CKMB2 = Convert.ToSingle(rdr["CPK_CKMB2"]);

                }
            }
                    m_dbConnection.Close();
        }
        
        private void enableAdminView(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(GroupBox) || c.GetType() == typeof(TextBox) || c.GetType() == typeof(MaskedTextBox) || c.GetType() == typeof(RadioButton) || c.GetType() == typeof(CheckBox))
                {
                    c.Enabled = true;
                }
                else
                {
                    enableAdminView(c);
                }
            }
        }

        private void disableAdminView(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(GroupBox))
                {
                    c.Enabled = false;
                }
                else
                {
                    disableAdminView(c);
                }
            }
        }

        private void hideTabs()
        {
            patHigPan.Visible = false;
            labsAndImaTabs.Visible = false;
            mARTabs.Visible = false;
            assDataTabs.Visible = false;
            notTabs.Visible = false;
            ordTabs.Visible = false;
        }

        private void hideAssSubCat()
        {
            genSurSubCatPan.Visible = false;
            hEENTSubCatPan.Visible = false;
            uppExtSubCatPan.Visible = false;
            pulSubCatPan.Visible = false;
            carSubCatPan.Visible = false;
            abdSubCatPan.Visible = false;
            lowExtSubCatPan.Visible = false;
            painSubCatPnl.Visible = false;
        }

        private void hideSafCheSubCat()
        {
            braScaSubCatPan.Visible = false;
            braScaResPan.Visible = false;
            morScaSubCatPan.Visible = false;
            morScaResSubCatPan.Visible = false;
        }
        private void hideHomeButtons()
        {
            patHigBtn.Visible = false;
            labsAndImaBtn.Visible = false;
            mARBtn.Visible = false;
            assDataBtn.Visible = false;
            notBtn.Visible = false;
            ordBtn.Visible = false;
        }
        private void showButtons()
        {
            patHigBtn.Visible = true;
            labsAndImaBtn.Visible = true;
            mARBtn.Visible = true;
            assDataBtn.Visible = true;
            notBtn.Visible = true;
            ordBtn.Visible = true;
        }
        private void userMenuItem_Click(object sender, EventArgs e)
        {
            addUser addUserForm = new addUser();
            addUserForm.Show();
        }

        private void setMenuItem_Click(object sender, EventArgs e)
        {
            adminSet adminSetForm = new adminSet();
            adminSetForm.Show();
        }

        private void logOutMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Hide();
                logOut logOutForm = new logOut();
                logOutForm.Show();
            }
        }

        private void sideButtonEnabled()
        {
            buttonMAR.Enabled = true;
            buttonPH.Enabled = true;
            buttonMAR.Enabled = true;
            buttonLI.Enabled = true;
            buttonAD.Enabled = true;
            buttonNotes.Enabled = true;
            buttonOrders.Enabled = true;
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            hideTabs();
            showButtons();
            selectionMenu.Visible = false;
            sideButtonEnabled();
        }

        private void buttonPH_Click(object sender, EventArgs e)
        {
            hideTabs();
            sideButtonEnabled();
            buttonPH.Enabled = false;
            selectionMenu.Visible = true;
            patHigPan.Visible = true;
        }

        private void buttonMAR_Click(object sender, EventArgs e)
        {
            hideTabs();
            sideButtonEnabled();
            buttonMAR.Enabled = false;
            selectionMenu.Visible = true;
            mARTabs.Visible = true;
        }

        private void buttonLI_Click(object sender, EventArgs e)
        {
            hideTabs();
            sideButtonEnabled();
            buttonLI.Enabled = false;
            selectionMenu.Visible = true;
            labsAndImaTabs.Visible = true;
        }

        private void buttonAD_Click(object sender, EventArgs e)
        {
            hideTabs();
            sideButtonEnabled();
            buttonAD.Enabled = false;
            selectionMenu.Visible = true;
            assDataTabs.Visible = true;
        }

        private void buttonNotes_Click(object sender, EventArgs e)
        {
            hideTabs();
            sideButtonEnabled();
            buttonNotes.Enabled = false;
            selectionMenu.Visible = true;
            notTabs.Visible = true;
        }

        private void buttonOrders_Click(object sender, EventArgs e)
        {
            hideTabs();
            sideButtonEnabled();
            buttonOrders.Enabled = false;
            selectionMenu.Visible = true;
            ordTabs.Visible = true;
        }

        private void patientDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string curName = patientDropDown.Text;
            loadPatient(curName);

            //Patient Data
            mRNMaskTxtBox.Text = curPatient.patientID.ToString();
            sexMaskTxtBox.Text = curPatient.sex.ToString();
            ageMaskTxtBox.Text = curPatient.age.ToString();
            birthMaskTxtBox.Text = curPatient.DOB.ToString("d");
            allMaskTxtBox.Text = curPatient.allergies;
            infPreMaskTxtBox.Text = curPatient.infections;

            //Labs
            naMaskTxtBox.Text = curPatient.sodium.ToString();
            labelNa.Text += ":\n" + curPatient.sodium.ToString();
            kMaskTxtBox.Text = curPatient.potassium.ToString();
            labelK.Text += ":\n" + curPatient.potassium.ToString();
            clMaskTxtBox.Text = curPatient.chloride.ToString();
            labelCl.Text += ":\n" + curPatient.chloride.ToString();
            HCO3MaskTxtBox.Text = curPatient.HCO3.ToString();
            labelHCO3.Text += ":\n" + curPatient.HCO3.ToString();
            bUNMaskTxtBox.Text = curPatient.BUN.ToString();
            labelBUN.Text += ":\n" + curPatient.BUN.ToString();
            creatMaskTxtBox.Text = curPatient.creatinine.ToString();
            labelCreatine.Text += ":\n" + curPatient.creatinine.ToString();
            glucoseMaskTxtBox.Text = curPatient.glucose.ToString();
            labelGlaucose.Text += ":\n" + curPatient.glucose.ToString();
            ca2MaskTxtBox.Text = curPatient.calcium.ToString();
            labelCa.Text += ":\n" + curPatient.calcium.ToString();
            textBox21.Text = curPatient.magnesium.ToString();
            textBox36.Text = curPatient.magnesium.ToString();
            PO43MaskTxtBox.Text = curPatient.phosphate.ToString();
            labelPO4.Text = curPatient.phosphate.ToString();
            tPMaskTxtBox.Text = curPatient.protein.ToString();
            labelTP.Text += ":\n" + curPatient.protein.ToString();
            albMaskTxtBox.Text = curPatient.albumin.ToString();
            labelAlb.Text += ":\n" + curPatient.albumin.ToString();
            aSTMaskTxtBox.Text = curPatient.AST.ToString();
            labelast.Text += ":\n" + curPatient.AST.ToString();
            aLPMaskTxtBox.Text = curPatient.ALP.ToString();
            label150.Text += ":\n" + curPatient.ALP.ToString();
            aLTMaskTxtBox.Text = curPatient.ALT.ToString();
            labelalt.Text += ":\n" + curPatient.ALT.ToString();
            lDHMastTxtBox.Text = curPatient.LDH.ToString();
            labelldh.Text += ":\n" + curPatient.LDH.ToString();
            bilMastTxtBox.Text = curPatient.bilirubin.ToString();
            labelBiliru.Text += ":\n" + curPatient.bilirubin.ToString();
            hCTMaskTxtBox.Text = curPatient.HCT.ToString();
            labelHCT.Text += ":\n" + curPatient.HCT.ToString();
            rBCMaskTxtBox.Text = curPatient.ALT.ToString();
            labelRBC.Text += ":\n" + curPatient.RBC.ToString();
            hgbMaskTxtBox.Text = curPatient.Hgb.ToString();
            labelHgb.Text += ":\n" + curPatient.Hgb.ToString();
            wBCMaskTxtBox.Text = curPatient.WBC.ToString();
            labelWBC.Text += ":\n" + curPatient.WBC.ToString();
            pLTMaskTxtBox.Text = curPatient.PLT.ToString();
            labelPLT.Text += ":\n" + curPatient.PLT.ToString();
            pTMaskTxtBox.Text = curPatient.PT.ToString();
            labelPT.Text += ":\n" + curPatient.PT.ToString();
            pTTMaskTxtBox.Text = curPatient.PTT.ToString();
            labelPTT.Text += ":\n" + curPatient.PTT.ToString();
            iNRMaskTxtBox.Text = curPatient.INR.ToString();
            labelINR.Text += ":\n" + curPatient.INR.ToString();
            myoglobinMaskTxtBox.Text = curPatient.myoglobin.ToString();
            cTnIMaskTxtBox.Text = curPatient.cTnI.ToString();
            cTnTMaskTxtBox.Text = curPatient.cTnT.ToString();
            cPKMaskTxtBox.Text = curPatient.CPK_CKMB2.ToString();
            

            //groupBox1.Visible = true;
            patHigBtn.Enabled = true;
            labsAndImaBtn.Enabled = true;
            mARBtn.Enabled = true;
            assDataBtn.Enabled = true;
            notBtn.Enabled = true;
            ordBtn.Enabled = true;
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void authorizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem1.Enabled = true;
            notAuthorizedToolStripMenuItem.Checked = false;
        }

        private void notAuthorizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem1.Enabled = false;
            authorizedToolStripMenuItem.Checked = false;
        }

        //Assessment buttons
        private void genSurSubCatBtn_Click(object sender, EventArgs e)
        {
            hideAssSubCat();
            genSurSubCatPan.Visible = true;
        }
        private void hEENTSubCatBtn_Click(object sender, EventArgs e)
        {
            hideAssSubCat();
            hEENTSubCatPan.Visible = true;
        }
        private void uppExtSubCatBtn_Click(object sender, EventArgs e)
        {
            hideAssSubCat();
            uppExtSubCatPan.Visible = true;
        }
        private void pulSubCatBtn_Click(object sender, EventArgs e)
        {
            hideAssSubCat();
            pulSubCatPan.Visible = true;
        }
        private void carSubCatBtn_Click(object sender, EventArgs e)
        {
            hideAssSubCat();
            carSubCatPan.Visible = true;
        }
        private void abdSubCatBtn_Click(object sender, EventArgs e)
        {
            hideAssSubCat();
            abdSubCatPan.Visible = true;
        }
        private void lowExtSubCatBtn_Click(object sender, EventArgs e)
        {
            hideAssSubCat();
            lowExtSubCatPan.Visible = true;
        }
        private void painSubCatBtn_Click(object sender, EventArgs e)
        {
            hideAssSubCat();
            painSubCatPnl.Visible = true;
        }

        // Safety Check Sub Category Buttons
        private void braScaSubCatBtn_Click(object sender, EventArgs e)
        {
            hideSafCheSubCat();
            braScaSubCatPan.Visible = true;
        }

        private void braScaResSubCatBtn_Click(object sender, EventArgs e)
        {
            hideSafCheSubCat();
            braScaResPan.Visible = true;
        }

        private void morScaSubCatBtn_Click(object sender, EventArgs e)
        {
            hideSafCheSubCat();
            morScaSubCatPan.Visible = true;
        }

        private void morScaResSubCatBtn_Click(object sender, EventArgs e)
        {
            hideSafCheSubCat();
            morScaResSubCatPan.Visible = true;
        }

        // Main Categories Buttons
        private void mARBtn_Click_2(object sender, EventArgs e)
        {
            //hideTabs();
            buttonMAR.Enabled = false;
            mARTabs.Visible = true;
            selectionMenu.Visible = true;
            hideHomeButtons();
        }

        private void patHigBtn_Click(object sender, EventArgs e)
        {
            // hideTabs();
            buttonPH.Enabled = false;
            selectionMenu.Visible = true;
            hideHomeButtons();
            patHigPan.Visible = true;
        }

        private void labsAndImaBtn_Click(object sender, EventArgs e)
        {
            // hideTabs();
            buttonLI.Enabled = false;
            selectionMenu.Visible = true;
            hideHomeButtons();
            labsAndImaTabs.Visible = true;
        }

        private void assDataBtn_Click(object sender, EventArgs e)
        {
            //hideTabs();
            buttonAD.Enabled = false;
            selectionMenu.Visible = true;
            hideHomeButtons();
            assDataTabs.Visible = true;
        }

        private void notBtn_Click(object sender, EventArgs e)
        {
            //hideTabs();
            buttonNotes.Enabled = false;
            selectionMenu.Visible = true;
            hideHomeButtons();
            notTabs.Visible = true;
        }

        private void ordBtn_Click(object sender, EventArgs e)
        {
            //hideTabs();
            buttonOrders.Enabled = false;
            selectionMenu.Visible = true;
            hideHomeButtons();
            ordTabs.Visible = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                textBox5.Enabled = true;
            }
            else
            {
                textBox5.Enabled = false;
            }
        }

        private void mainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit EMPRS?", "Exit EMPRS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (global.isAdmin == false) // Restoring the controls to a "fresh state"
                {
                    ordTabs.TabPages.Add(tabPage3);
                }
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void medTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            maskedTextBox39.ReadOnly = false;
            maskedTextBox38.ReadOnly = false;
            maskedTextBox41.ReadOnly = false;
            ordMed_Time_TxtBox.ReadOnly = false;
        }

        private void ca2MaskTxtBox_Leave(object sender, EventArgs e)
        {
            if (float.Parse(ca2MaskTxtBox.Text) < 8.6)
            {
                ca2MaskTxtBox.ForeColor = System.Drawing.Color.Navy;
            }
            else if (float.Parse(ca2MaskTxtBox.Text) > 10.2)
            {
                ca2MaskTxtBox.ForeColor = System.Drawing.Color.Crimson;
            }
        }

        private void tPMaskTxtBox_Leave(object sender, EventArgs e)
        {
            if (float.Parse(tPMaskTxtBox.Text) < 8.6)
            {
                tPMaskTxtBox.ForeColor = System.Drawing.Color.Navy;
            }
            else if (float.Parse(ca2MaskTxtBox.Text) > 10.2)
            {
                tPMaskTxtBox.ForeColor = System.Drawing.Color.Crimson;
            }
        }
    }
}

