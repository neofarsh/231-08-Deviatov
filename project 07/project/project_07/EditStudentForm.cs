using StudentManager.Models;
using StudentManager.Services;
using System;
using System.Windows.Forms;

namespace StudentManager.Forms
{
    public partial class EditStudentForm : Form
    {
        public Student Student { get; private set; }

        public EditStudentForm() : this(new Student()) { }

        public EditStudentForm(Student student)
        {
            InitializeComponent();
            Student = student;
            InitializeControls();
        }

        private void InitializeControls()
        {
            dtpBirthDate.Format = DateTimePickerFormat.Custom;
            dtpBirthDate.CustomFormat = "dd.MM.yyyy";
            dtpBirthDate.MinDate = new DateTime(1991, 12, 25);
            dtpBirthDate.MaxDate = DateTime.Today;

            if (Student.Id > 0)
            {
                txtLastName.Text = Student.LastName;
                txtFirstName.Text = Student.FirstName;
                txtMiddleName.Text = Student.MiddleName;
                numCourse.Value = Student.Course;
                txtGroup.Text = Student.Group;
                dtpBirthDate.Value = Student.BirthDate;
                txtEmail.Text = Student.Email;
                txtPhone.Text = Student.Phone;
            }

            btnSave.Click += (s, e) => SaveStudent();
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        private void SaveStudent()
        {
            if (!ValidateInputs()) return;

            Student.LastName = txtLastName.Text.Trim();
            Student.FirstName = txtFirstName.Text.Trim();
            Student.MiddleName = txtMiddleName.Text.Trim();
            Student.Course = (int)numCourse.Value;
            Student.Group = txtGroup.Text.Trim();
            Student.BirthDate = dtpBirthDate.Value;
            Student.Email = txtEmail.Text.Trim();
            Student.Phone = txtPhone.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }


        private bool ValidateInputs()
        {
            errorProvider.Clear();

            bool isValid = true;

            if (!ValidatorService.ValidateName(txtLastName.Text))
            {
                errorProvider.SetError(txtLastName, "Фамилия должна содержать минимум 2 символа");
                isValid = false;
            }

            if (!ValidatorService.ValidateName(txtFirstName.Text))
            {
                errorProvider.SetError(txtFirstName, "Имя должно содержать минимум 2 символа");
                isValid = false;
            }

            if (!ValidatorService.ValidateCourse((int)numCourse.Value))
            {
                errorProvider.SetError(numCourse, "Курс должен быть от 1 до 6");
                isValid = false;
            }

            if (!ValidatorService.ValidateGroup(txtGroup.Text))
            {
                errorProvider.SetError(txtGroup, "Группа должна содержать минимум 2 символа");
                isValid = false;
            }

            if (!ValidatorService.ValidateEmail(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Некорректный email. Допустимые домены: yandex.ru, gmail.com, icloud.com");
                isValid = false;
            }

            if (!ValidatorService.ValidatePhone(txtPhone.Text))
            {
                errorProvider.SetError(txtPhone, "Телефон должен быть в формате +7-XXX-XXX-XX-XX");
                isValid = false;
            }

            return isValid;
        }
    }
}
