using Stockapp.Data;
using Stockapp.Data.Access;
using Stockapp.Data.Entities;
using Stockapp.Data.Exceptions;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using Stockapp.Logic.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stockapp.DesktopApp
{
    public partial class DesktopApp : Form
    {
        private readonly IUserLogic userLogic;
        private readonly IAdminLogic adminLogic;
        private readonly IStockLogic stockLogic;
        private readonly IGameSettingsLogic gameSettingsLogic;
        private Admin actualAdmin;

        public DesktopApp()
        {
            InitializeComponent();
            Context context = new Context();
            IUnitOfWork uw = new UnitOfWork(context);
            userLogic = new UserLogic(uw);
            adminLogic = new AdminLogic(uw);
            stockLogic = new StockLogic(uw);
            gameSettingsLogic = new GameSettingsLogic(uw);
            actualAdmin = new Admin();
            panelOptions.Visible = false;
            panelAdminMaintenace.Visible = false;
            panelCreateStock.Visible = false;
            panelModifyGameConditions.Visible = false;
            panelCreateAdmin.Visible = false;
            panelModifyDeleteStock.Visible = false;
            textBoxPassword.PasswordChar = '*';
            textBoxPasswordMaintenance.PasswordChar = '*';
            textBoxCreateAdminPassword.PasswordChar = '*';

            textBoxEmail.Text = "arto@gmail.com";
            textBoxPassword.Text = "Artoo.1234554";
        }

        private void AdminMaintenance()
        {
            listBoxAdmins.DataSource = null;
            listBoxAdmins.DataSource = adminLogic.GetAll().ToList();
            listBoxAdmins.SetSelected(0, true);
            Admin admin = (Admin)listBoxAdmins.SelectedItem;
            textBoxCI.Text = admin.CI.ToString();
            textBoxName.Text = admin.Name;
            textBoxSurname.Text = admin.Surname;
            textBoxEmailMaintenance.Text = admin.Email;
            textBoxPasswordMaintenance.Text = admin.User.Password;
        }

        private void StockMaintenance()
        {
            listBoxStocks.DataSource = null;
            listBoxStocks.DataSource = stockLogic.GetAllStocks().ToList();
            listBoxStocks.SetSelected(0, true);
            Stock stock = (Stock)listBoxStocks.SelectedItem;
            textBoxMaintainanceCode.Text = stock.Code;
            textBoxMaintainanceName.Text = stock.Name;
            textBoxMaintainanceDescription.Text = stock.Description;
            textBoxMaintainanceActions.Text = stock.QuantiyOfActions.ToString();
            textBoxMaintainanceInitialValue.Text = stock.UnityValue.ToString();
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text;
            string password = textBoxPassword.Text;
            User user = new User();
            user.Email = email;
            user.Password = password;
            User searchedUser = null;
            bool badPassword = false;
            try
            {
                searchedUser = userLogic.LogIn(user);
            }
            catch (UserException ex)
            {
                badPassword = true;
                MessageBox.Show(ex.Message);
            }
            if (searchedUser != null && searchedUser.IsAdmin)
            {
                actualAdmin = adminLogic.GetUserAdmin(searchedUser.Id);
                panelSignIn.Visible = false;
                panelOptions.Visible = true;
            }
            else
            {
                if (searchedUser == null && !badPassword)
                {
                    MessageBox.Show("El usuario no existe", "Error");
                }
                else if (searchedUser != null && !searchedUser.IsAdmin && !badPassword)
                {
                    MessageBox.Show("El usuario no es de tipo administrador", "Error");
                }
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            int ci = 0;
            try
            {
                ci = Int32.Parse(textBoxCI.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("La CI debe ser un número","Error");
                return;
            }
            string name = textBoxName.Text;
            string surname = textBoxSurname.Text;
            string email = textBoxEmailMaintenance.Text;
            string password = textBoxPasswordMaintenance.Text;
            Admin admin = (Admin)listBoxAdmins.SelectedItem;
            admin.CI = ci;
            admin.Name = name;
            admin.Surname = surname;
            admin.Email = email;
            admin.User.Name = name;
            admin.User.Email = email;
            admin.User.Password = password;
            bool update = adminLogic.UpdateAdmin(admin);
            if (update)
            {
                MessageBox.Show("Administrador modificado correctamente", "Confirmación");
                panelAdminMaintenace.Visible = false;
                panelOptions.Visible = true;
            }
            else
            {
                if (!adminLogic.IsValidEmail(email))
                {
                    MessageBox.Show("El email es incorrecto", "Error");
                }
                else
                {
                    MessageBox.Show("El email ya está en uso", "Error");
                }
            }
        }

        private void ClearSignIn()
        {
            textBoxEmail.Clear();
            textBoxPassword.Clear();
        }

        private void ClearCreateAdmin()
        {
            textBoxCreateAdminCI.Clear();
            textBoxCreateAdminName.Clear();
            textBoxCreateAdminSurname.Clear();
            textBoxCreateAdminEmail.Clear();
            textBoxCreateAdminPassword.Clear();
        }

        private void ClearCreateStock()
        {
            textBoxStockCode.Clear();
            textBoxStockName.Clear();
            textBoxStockDescription.Clear();
            textBoxStockActions.Clear();
            textBoxStockUnityValue.Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Admin admin = (Admin)listBoxAdmins.SelectedItem;
            adminLogic.DeleteAdmin(admin);
            bool deleteUser = userLogic.DeleteUser(admin.UserId);
            MessageBox.Show("Se eliminó el administrador correctamente", "Confirmación");
            panelAdminMaintenace.Visible = false;
            if (admin.Id == actualAdmin.Id)
            {
                ClearSignIn();
                panelSignIn.Visible = true;
            }
            else
            {
                panelOptions.Visible = true;
            }
        }

        private void buttonBackMaintenance_Click(object sender, EventArgs e)
        {
            panelAdminMaintenace.Visible = false;
            panelOptions.Visible = true;
        }

        private void buttonOptionMaintenance_Click(object sender, EventArgs e)
        {
            panelOptions.Visible = false;
            AdminMaintenance();
            panelAdminMaintenace.Visible = true;
        }

        private void buttonBackStock_Click(object sender, EventArgs e)
        {
            panelCreateStock.Visible = false;
            panelModifyDeleteStock.Visible = true;
        }

        private void buttonDefineStock_Click(object sender, EventArgs e)
        {
            string code = textBoxStockCode.Text;
            string name = textBoxStockName.Text;
            string description = textBoxStockDescription.Text;
            string uv = textBoxStockUnityValue.Text;
            string actions = textBoxStockActions.Text;
            double unityValue = 0;
            double quantityOfActions = 0;
            try
            {
                unityValue = Double.Parse(uv);
                quantityOfActions = Double.Parse(actions);
            }
            catch (Exception)
            {
                MessageBox.Show("El valor inicial y la cantidad de acciones deben ser números","Error");
                return;
            }
            Stock stock = new Stock();
            stock.Code = code;
            stock.Name = name;
            stock.Description = description;
            stock.QuantiyOfActions = quantityOfActions;
            stock.UnityValue = unityValue;
            if (code == string.Empty || name == string.Empty || description == string.Empty)
            {
                MessageBox.Show("No debe dejar campos vacíos", "Error");
                return;
            }

            bool createStock = stockLogic.CreateStock(stock);
            if (createStock)
            {
                MessageBox.Show("Stock creado correctamente", "Confirmación");
                panelCreateStock.Visible = false;
                StockMaintenance();
                ClearCreateStock();
                panelModifyDeleteStock.Visible = true;
            }
            else
            {
                MessageBox.Show("El largo del código del stock debe ser menor o igual a 6", "Error");
            }
        }

        private void buttonBackGameConditions_Click(object sender, EventArgs e)
        {
            panelModifyGameConditions.Visible = false;
            panelOptions.Visible = true;
        }

        private void buttonOptionStock_Click(object sender, EventArgs e)
        {
            panelOptions.Visible = false;
            StockMaintenance();
            panelModifyDeleteStock.Visible = true;
        }

        private void buttonOptionGameConditions_Click(object sender, EventArgs e)
        {
            gameSettings();
            panelOptions.Visible = false;
            panelModifyGameConditions.Visible = true;
        }

        private void buttonSignOut_Click(object sender, EventArgs e)
        {
            panelOptions.Visible = false;
            panelSignIn.Visible = true;
        }

        private void gameSettings()
        {
            GameSettings gameSettings = gameSettingsLogic.Get();
            textBoxInitialMoneyGS.Text = gameSettings.InitialMoney.ToString();
            textBoxMaxTransactionsPerDay.Text = gameSettings.MaxTransactionsPerDay.ToString();
            textBoxRecomendationAlgoritm.Text = gameSettings.RecomendationAlgorithm;
        }

        private void buttonModifyGameConditions_Click(object sender, EventArgs e)
        {
            GameSettings gameSettings = gameSettingsLogic.Get();
            string im = textBoxInitialMoneyGS.Text;
            string ms = textBoxMaxTransactionsPerDay.Text;
            double initialMoney = 0;
            int maxTransactionsPerDay = 0;
            try
            {
                initialMoney = Double.Parse(im);
                maxTransactionsPerDay = Int32.Parse(ms);
            }
            catch (Exception)
            {
                MessageBox.Show("La cantidad inicial de dinero y el máximo de transacciones por día deben ser números", "Error");
                return;
            }
            string recomendationAlgoritm = textBoxRecomendationAlgoritm.Text;
            if (recomendationAlgoritm == string.Empty)
            {
                MessageBox.Show("El algoritmo de recomendación no puede ser vacío", "Error");
                return;
            }
            gameSettings.InitialMoney = initialMoney;
            gameSettings.MaxTransactionsPerDay = maxTransactionsPerDay;
            gameSettings.RecomendationAlgorithm = recomendationAlgoritm;
            gameSettingsLogic.UpdateOrCreateGameSettings(gameSettings);
            MessageBox.Show("Condiciones del juego modificadas correctamente", "Confirmación");
            panelModifyGameConditions.Visible = false;
            panelOptions.Visible = true;
        }

        private void listBoxAdmins_SelectedIndexChanged(object sender, EventArgs e)
        {
            Admin admin = (Admin)listBoxAdmins.SelectedItem;
            if (admin != null)
            {
                textBoxCI.Text = admin.CI.ToString();
                textBoxName.Text = admin.Name;
                textBoxSurname.Text = admin.Surname;
                textBoxEmailMaintenance.Text = admin.Email;
                textBoxPasswordMaintenance.Text = admin.User.Password;
            }
        }

        private void buttonCreateAdminBack_Click(object sender, EventArgs e)
        {
            panelCreateAdmin.Visible = false;
            panelAdminMaintenace.Visible = true;
        }

        private void buttonCreateAdminOk_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            User user = new User();
            int ci = 0;
            try
            {
                ci = Int32.Parse(textBoxCreateAdminCI.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("La CI debe ser un número", "Error");
                return;
            }
            string name = textBoxCreateAdminName.Text;
            string surname = textBoxCreateAdminSurname.Text;
            string email = textBoxCreateAdminEmail.Text;
            string password = textBoxCreateAdminPassword.Text;
            if (name == string.Empty || surname == string.Empty || email == string.Empty || password == string.Empty)
            {
                MessageBox.Show("No debe dejar campos vacíos", "Error");
                return;    
            }
            if (!userLogic.EmailIsUnique(email))
            {
                MessageBox.Show("El email ya está en uso", "Error");
                return;
            }
            if (!userLogic.ValidPasswordLenght(password))
            {
                MessageBox.Show("El largo de la contraseña debe ser mayor o igual a 6", "Error");
                return;
            }
            if (!adminLogic.CIIsUnique(ci))
            {
                MessageBox.Show("La cédula ya está en uso", "Error");
                return;
            }
            if (!adminLogic.IsValidEmail(email))
            {
                MessageBox.Show("El email es incorrecto", "Error");
                return;
            }
            user.Name = name;
            user.Email = email;
            user.Password = password;
            user.IsAdmin = true;
            userLogic.RegisterWindowsForm(user);

            admin.CI = ci;
            admin.Name = name;
            admin.Surname = surname;
            admin.Email = email;
            admin.UserId = user.Id;
            bool createAdmin = adminLogic.CreateAdmin(admin);
            if (createAdmin)
            {
                MessageBox.Show("Se creo el administrador correctamente", "Confirmación");
            }
            panelCreateAdmin.Visible = false;
            AdminMaintenance();
            ClearCreateAdmin();
            panelAdminMaintenace.Visible = true;
        }

        private void buttonCreateAdmin_Click(object sender, EventArgs e)
        {
            panelAdminMaintenace.Visible = false;
            panelCreateAdmin.Visible = true;
        }

        private void buttonStockMaintainanceDefine_Click(object sender, EventArgs e)
        {
            panelModifyDeleteStock.Visible = false;
            panelCreateStock.Visible = true;
        }

        private void listBoxStocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            Stock stock = (Stock)listBoxStocks.SelectedItem;
            if (stock != null)
            {
                textBoxMaintainanceCode.Text = stock.Code;
                textBoxMaintainanceName.Text = stock.Name;
                textBoxMaintainanceDescription.Text = stock.Description;
                textBoxMaintainanceActions.Text = stock.QuantiyOfActions.ToString();
                textBoxMaintainanceInitialValue.Text = stock.UnityValue.ToString();
            }
        }

        private void buttonStockMaintainanceDelete_Click(object sender, EventArgs e)
        {
            Stock stock = (Stock)listBoxStocks.SelectedItem;
            bool deleteStock = stockLogic.DeleteStock(stock);
            if (deleteStock)
            {
                MessageBox.Show("Se eliminó el stock correctamente", "Confirmación");
            }
            else
            {
                MessageBox.Show("Ocurrió un error en la eliminación del stock", "Error");
                return;
            }
            panelModifyDeleteStock.Visible = false;
            panelOptions.Visible = true;
        }

        private void buttonStockMaintainanceModify_Click(object sender, EventArgs e)
        {
            string code = textBoxMaintainanceCode.Text;
            string name = textBoxMaintainanceName.Text;
            string description = textBoxMaintainanceDescription.Text;
            string uv = textBoxMaintainanceInitialValue.Text;
            string actions = textBoxMaintainanceActions.Text;
            double unityValue = 0;
            double quantityOfActions = 0;
            try
            {
                unityValue = Double.Parse(uv);
                quantityOfActions = Double.Parse(actions);
            }
            catch (Exception)
            {
                MessageBox.Show("El valor inicial y la cantidad de acciones deben ser números", "Error");
                return;
            }
            Stock stock = (Stock)listBoxStocks.SelectedItem;
            stock.Code = code.ToUpper();
            stock.Name = name;
            stock.Description = description;
            stock.QuantiyOfActions = quantityOfActions;
            stock.UnityValue = unityValue;
            if (code == string.Empty || name == string.Empty || description == string.Empty)
            {
                MessageBox.Show("No debe dejar campos vacíos", "Error");
                return;
            }
            bool update = stockLogic.UpdateStock(stock);
            if (update)
            {
                MessageBox.Show("Stock modificado correctamente", "Confirmación");
                panelModifyDeleteStock.Visible = false;
                panelOptions.Visible = true;
            }
            else
            {
                MessageBox.Show("Ocurrió un error en la modificación del stock", "Error");
            }
        }

        private void buttonStockMaintainanceBack_Click(object sender, EventArgs e)
        {
            panelModifyDeleteStock.Visible = false;
            panelOptions.Visible = true;
        }
    }
}
