using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c_sharp_serializer_gabee1987
{
    public partial class Serializer : Form
    {
        int serialNumber;

        public Serializer()
        {
            InitializeComponent();
        }

        #region Events

        private void Serializer_Load(object sender, EventArgs e)
        {
            serialNumber = 1;
            string fileInfo = "person" + serialNumber + ".dat";

            // Set ErrorProviders
            SetNameErrorProviderToTextBox(NameTextBox);
            SetPhoneErrorProviderToTextBox(PhoneTextBox);
            SetAddressErrorProviderToTextBox(AddressTextBox);

            // Load person Information
            LoadAndDisplayPersonInfo(fileInfo);
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (RegexValidation.IsNameValid(NameTextBox.Text))
            {
                nameErrorProvider.SetError(NameTextBox, String.Empty);
            }
            else
            {
                nameErrorProvider.SetError(NameTextBox, "Invalid name format.");
            }
        }

        private void PhoneTextBox_TextChanged(object sender, EventArgs e)
        {
            if (RegexValidation.IsPhoneValid(PhoneTextBox.Text))
            {
                nameErrorProvider.SetError(PhoneTextBox, String.Empty);
            }
            else
            {
                nameErrorProvider.SetError(PhoneTextBox, "Invalid phone number format.");
            }
        }

        private void AddressTextBox_TextChanged(object sender, EventArgs e)
        {
            if (RegexValidation.IsAddressValid(AddressTextBox.Text))
            {
                nameErrorProvider.SetError(AddressTextBox, String.Empty);
            }
            else
            {
                nameErrorProvider.SetError(AddressTextBox, "Invalid email format.");
            }
        }

        private void PhoneTextBox_Leave(object sender, EventArgs e)
        {
            string originalPhoneTextBoxText = PhoneTextBox.Text;
            PhoneTextBox.Text = RegexValidation.ReformatPhoneNumber(originalPhoneTextBoxText);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string fileInfo = "person" + serialNumber + ".dat";
            string name = NameTextBox.Text;
            string phoneNumber = PhoneTextBox.Text;
            string address = AddressTextBox.Text;
            DateTime creationDate = DateTime.Now;

            Person personToSave = new Person(name, address, phoneNumber,  creationDate);
            Person.SerializePerson(personToSave, fileInfo);
            CreationDateLabel.Text = personToSave.CreationDate.ToString();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            serialNumber++;
            string fileInfo = "person" + serialNumber + ".dat";
            LoadAndDisplayPersonInfo(fileInfo);
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            serialNumber--;
            string fileInfo = "person" + serialNumber + ".dat";
            LoadAndDisplayPersonInfo(fileInfo);
        }

        #endregion

        #region ErrorProviders

        // Create and set the nameErrorProvider for a textbox to it's right side
        private void SetNameErrorProviderToTextBox(TextBox textBoxToAdd)
        {
            nameErrorProvider = new ErrorProvider();
            nameErrorProvider.SetIconAlignment(textBoxToAdd, ErrorIconAlignment.MiddleRight);
            nameErrorProvider.SetIconPadding(textBoxToAdd, 2);
            nameErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void SetPhoneErrorProviderToTextBox(TextBox textBoxToAdd)
        {
            phoneErrorProvider = new ErrorProvider();
            phoneErrorProvider.SetIconAlignment(textBoxToAdd, ErrorIconAlignment.MiddleRight);
            phoneErrorProvider.SetIconPadding(textBoxToAdd, 2);
            phoneErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void SetAddressErrorProviderToTextBox(TextBox textBoxToAdd)
        {
            addressErrorProvider = new ErrorProvider();
            addressErrorProvider.SetIconAlignment(textBoxToAdd, ErrorIconAlignment.MiddleRight);
            addressErrorProvider.SetIconPadding(textBoxToAdd, 2);
            addressErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        #endregion

        #region Basic Methods

        private void LoadAndDisplayPersonInfo(String fileInfo)
        {
            try
            {
                Person personToShow = Person.DeserializePerson(fileInfo);
                string name = personToShow.Name;
                string phoneNumber = personToShow.PhoneNumber;
                string address = personToShow.Address;
                DateTime creationDate = personToShow.CreationDate;

                NameTextBox.Text = name;
                PhoneTextBox.Text = phoneNumber;
                AddressTextBox.Text = address;
                CreationDateLabel.Text = creationDate.ToString();
                
            }
            catch (NullReferenceException ne)
            {
                Console.WriteLine(ne);
            }
        }

        #endregion
    }
}
