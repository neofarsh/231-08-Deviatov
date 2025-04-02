using StudentManager.Data;
using StudentManager.Models;
using StudentManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace StudentManager.Forms
{
    public partial class MainForm : Form
    {
        private readonly IStudentRepository _repository;
        private bool _unsavedChanges = false;
        private string _currentFilePath = null;

        public MainForm(IStudentRepository repository)
        {
            InitializeComponent();
            _repository = repository;
            InitializeDataGrid();
            InitializeControls();
            UpdateStats();
        }

        private void InitializeDataGrid()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.Columns.Add("Id", "ID");
            dataGridView.Columns.Add("LastName", "Фамилия");
            dataGridView.Columns.Add("FirstName", "Имя");
            dataGridView.Columns.Add("MiddleName", "Отчество");
            dataGridView.Columns.Add("Course", "Курс");
            dataGridView.Columns.Add("Group", "Группа");
            dataGridView.Columns.Add("BirthDate", "Дата рождения");
            dataGridView.Columns.Add("Email", "Email");
            dataGridView.Columns.Add("Phone", "Телефон");

            dataGridView.Columns["Id"].DataPropertyName = "Id";
            dataGridView.Columns["LastName"].DataPropertyName = "LastName";
            dataGridView.Columns["FirstName"].DataPropertyName = "FirstName";
            dataGridView.Columns["MiddleName"].DataPropertyName = "MiddleName";
            dataGridView.Columns["Course"].DataPropertyName = "Course";
            dataGridView.Columns["Group"].DataPropertyName = "Group";
            dataGridView.Columns["BirthDate"].DataPropertyName = "BirthDate";
            dataGridView.Columns["Email"].DataPropertyName = "Email";
            dataGridView.Columns["Phone"].DataPropertyName = "Phone";

            dataGridView.Columns["BirthDate"].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        private void InitializeControls()
        {
            // Настройка кнопок
            btnAdd.Click += (s, e) => AddStudent();
            btnEdit.Click += (s, e) => EditStudent();
            btnDelete.Click += (s, e) => DeleteStudent();
            btnSave.Click += (s, e) => SaveData();
            btnLoad.Click += (s, e) => LoadData();
            btnExport.Click += (s, e) => ExportData();
            btnImport.Click += (s, e) => ImportData();
            btnRefresh.Click += (s, e) => RefreshData();

            // Настройка поиска
            txtSearch.TextChanged += (s, e) => ApplyFilters();
            cmbCourseFilter.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbGroupFilter.SelectedIndexChanged += (s, e) => ApplyFilters();

            // Настройка сортировки
            cmbSortBy.SelectedIndexChanged += (s, e) => ApplySorting();
            rbAscending.CheckedChanged += (s, e) => ApplySorting();
            rbDescending.CheckedChanged += (s, e) => ApplySorting();

            // Заполнение фильтров
            cmbCourseFilter.Items.Add("Все курсы");
            cmbGroupFilter.Items.Add("Все группы");
            cmbSortBy.Items.AddRange(new[] { "Фамилия", "Группа", "Курс", "Дата рождения" });

            cmbCourseFilter.SelectedIndex = 0;
            cmbGroupFilter.SelectedIndex = 0;
            cmbSortBy.SelectedIndex = 0;
            rbAscending.Checked = true;
        }

        private void LoadStudents()
        {
            var students = _repository.GetAll();
            dataGridView.DataSource = new BindingList<Student>(students);
            UpdateFilters(students);
            UpdateStats();
            _unsavedChanges = false;
        }

        private void UpdateFilters(List<Student> students)
        {
            var currentCourse = cmbCourseFilter.SelectedItem?.ToString();
            var currentGroup = cmbGroupFilter.SelectedItem?.ToString();

            cmbCourseFilter.Items.Clear();
            cmbGroupFilter.Items.Clear();

            cmbCourseFilter.Items.Add("Все курсы");
            cmbGroupFilter.Items.Add("Все группы");

            cmbCourseFilter.Items.AddRange(students
                .Select(s => s.Course.ToString())
                .Distinct()
                .OrderBy(c => c)
                .ToArray());

            cmbGroupFilter.Items.AddRange(students
                .Select(s => s.Group)
                .Distinct()
                .OrderBy(g => g)
                .ToArray());

            if (currentCourse != null && cmbCourseFilter.Items.Contains(currentCourse))
                cmbCourseFilter.SelectedItem = currentCourse;
            else
                cmbCourseFilter.SelectedIndex = 0;

            if (currentGroup != null && cmbGroupFilter.Items.Contains(currentGroup))
                cmbGroupFilter.SelectedItem = currentGroup;
            else
                cmbGroupFilter.SelectedIndex = 0;
        }

        private void UpdateStats()
        {
            var courseStats = _repository.GetCourseStats();
            var groupStats = _repository.GetGroupStats();

            lblCourseStats.Text = string.Join("\n", courseStats.Select(kv => $"{kv.Key}: {kv.Value}"));
            lblGroupStats.Text = string.Join("\n", groupStats.Select(kv => $"{kv.Key}: {kv.Value}"));
        }

        private void ApplyFilters()
        {
            var searchQuery = txtSearch.Text.Trim();
            var courseFilter = cmbCourseFilter.SelectedItem?.ToString() == "Все курсы" ? null : cmbCourseFilter.SelectedItem?.ToString();
            var groupFilter = cmbGroupFilter.SelectedItem?.ToString() == "Все группы" ? null : cmbGroupFilter.SelectedItem?.ToString();

            List<Student> filteredStudents;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                filteredStudents = _repository.Search(searchQuery);
            }
            else
            {
                filteredStudents = _repository.Filter(courseFilter, groupFilter);
            }

            dataGridView.DataSource = new BindingList<Student>(filteredStudents);
            ApplySorting();
        }

        private void ApplySorting()
        {
            if (dataGridView.DataSource == null) return;

            var bindingList = (BindingList<Student>)dataGridView.DataSource;
            var sortBy = cmbSortBy.SelectedItem?.ToString();
            var ascending = rbAscending.Checked;

            List<Student> sortedStudents;

            switch (sortBy)
            {
                case "Фамилия":
                    sortedStudents = ascending ?
                        bindingList.OrderBy(s => s.LastName).ToList() :
                        bindingList.OrderByDescending(s => s.LastName).ToList();
                    break;

                case "Группа":
                    sortedStudents = ascending ?
                        bindingList.OrderBy(s => s.Group).ToList() :
                        bindingList.OrderByDescending(s => s.Group).ToList();
                    break;

                case "Курс":
                    sortedStudents = ascending ?
                        bindingList.OrderBy(s => s.Course).ToList() :
                        bindingList.OrderByDescending(s => s.Course).ToList();
                    break;

                case "Дата рождения":
                    sortedStudents = ascending ?
                        bindingList.OrderBy(s => s.BirthDate).ToList() :
                        bindingList.OrderByDescending(s => s.BirthDate).ToList();
                    break;

                default:
                    sortedStudents = bindingList.ToList();
                    break;
            }

            dataGridView.DataSource = new BindingList<Student>(sortedStudents);
        }

        private void AddStudent()
        {
            using (var form = new EditStudentForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _repository.Add(form.Student);
                    _unsavedChanges = true;
                    LoadStudents();
                }
            }
        }

        private void EditStudent()
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            var studentId = (int)dataGridView.SelectedRows[0].Cells["Id"].Value;
            var student = _repository.GetById(studentId);

            if (student == null) return;

            using (var form = new EditStudentForm(student))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _repository.Update(form.Student);
                    _unsavedChanges = true;
                    LoadStudents();
                }
            }
        }

        private void DeleteStudent()
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            var studentId = (int)dataGridView.SelectedRows[0].Cells["Id"].Value;
            var student = _repository.GetById(studentId);

            if (student == null) return;

            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить студента {student.LastName} {student.FirstName}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _repository.Delete(studentId);
                _unsavedChanges = true;
                LoadStudents();
            }
        }

        private void SaveData()
        {
            if (_currentFilePath == null)
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        _currentFilePath = saveDialog.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            try
            {
                _repository.SaveToJson(_currentFilePath);
                _unsavedChanges = false;
                MessageBox.Show("Данные успешно сохранены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            if (_unsavedChanges)
            {
                var result = MessageBox.Show(
                    "Есть несохраненные изменения. Продолжить загрузку?",
                    "Предупреждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _repository.LoadFromJson(openDialog.FileName);
                        _currentFilePath = openDialog.FileName;
                        _unsavedChanges = false;
                        LoadStudents();
                        MessageBox.Show("Данные успешно загружены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportData()
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _repository.ExportToCsv(saveDialog.FileName);
                        MessageBox.Show("Данные успешно экспортированы", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ImportData()
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _repository.ImportFromCsv(openDialog.FileName);
                        _unsavedChanges = true;
                        LoadStudents();
                        MessageBox.Show("Данные успешно импортированы", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при импорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void RefreshData()
        {
            LoadStudents();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_unsavedChanges)
            {
                var result = MessageBox.Show(
                    "Есть несохраненные изменения. Сохранить перед выходом?",
                    "Предупреждение",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveData();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
