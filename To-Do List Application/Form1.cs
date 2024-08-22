namespace To_Do_List_Application

{
    using System.Net;
    using System.Net.Mail;

    public partial class Form1 : Form
    {
        private List<Task> tasks = new List<Task>();
        private string smtpServer = "smtp-mail.outlook.com"; // Replace with your SMTP server
        private int smtpPort = 587; // Replace with the correct port for your SMTP server
        private string senderEmail = "your-Email"; // Replace with your email
        private string senderPassword = "Your-Password"; // Replace with your email password

        private System.Windows.Forms.Timer reminderTimer;
        private System.Windows.Forms.Timer lowPriorityTimer;
        private System.Windows.Forms.Timer mediumPriorityTimer;
        private System.Windows.Forms.Timer highPriorityTimer;

        public Form1()
        {


            InitializeComponent();
            InitializeCategories();
            InitializePriorities();
            InitializeListView(); // Initialize ListView columns
            SetupPriorityTimers();


            // Set initial button colors
            ConfigureButtonColors();


        }

        private void ConfigureButtonColors()
        {
            btnAddTask.BackColor = Color.Green;
            btnEditTask.BackColor = Color.Blue;
            btnDeleteTask.BackColor = Color.Red;

            btnAddTask.ForeColor = Color.White;
            btnEditTask.ForeColor = Color.White;
            btnDeleteTask.ForeColor = Color.White;
        }
        private void InitializeListView()
        {
            listViewTasks.View = View.Details; // Set the view mode
            listViewTasks.Columns.Add("Task Name", 150); // Width of 150 pixels
            listViewTasks.Columns.Add("Category", 100);
            listViewTasks.Columns.Add("Priority", 80);
            listViewTasks.Columns.Add("Due Date", 100);

            // Optional: Set sorting
            listViewTasks.Sorting = SortOrder.Ascending;
        }

        private void SetupPriorityTimers()
        {
            lowPriorityTimer = new System.Windows.Forms.Timer();
           lowPriorityTimer.Interval = 3600000; // Check every hour
            lowPriorityTimer.Tick += lowPriorityTimer_Tick;
            lowPriorityTimer.Start();

            mediumPriorityTimer = new System.Windows.Forms.Timer();
             mediumPriorityTimer.Interval = 1800000; // Check every 30 minutes
            mediumPriorityTimer.Tick += mediumPriorityTimer_Tick;
            mediumPriorityTimer.Start();

            highPriorityTimer = new System.Windows.Forms.Timer();
            highPriorityTimer.Interval = 600000; // Check every 10 minutes
            highPriorityTimer.Tick += highPriorityTimer_Tick;
            highPriorityTimer.Start();
        }

        private void SendEmailReminder(Task task)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail);
                    mail.To.Add(senderEmail); // Send the email to yourself or another email address
                    mail.Subject = $"Reminder: {task.Name} - {task.Priority} Priority";

                    switch (task.Priority)
                    {
                        case "Low":
                            mail.Body = $"This is a reminder for your low-priority task:\n\nTask: {task.Name}\nCategory: {task.Category}\nDue Date: {task.DueDate.ToShortDateString()}\n\nIt's a low-priority task, but don't forget to complete it!";
                            break;
                        case "Medium":
                            mail.Body = $"This is a reminder for your medium-priority task:\n\nTask: {task.Name}\nCategory: {task.Category}\nDue Date: {task.DueDate.ToShortDateString()}\n\nPlease make sure to work on this task soon!";
                            break;
                        case "High":
                            mail.Body = $"URGENT: High-priority task due soon!\n\nTask: {task.Name}\nCategory: {task.Category}\nDue Date: {task.DueDate.ToShortDateString()}\n\nThis is a high-priority task. Please address it as soon as possible!";
                            break;
                        default:
                            mail.Body = $"Task: {task.Name}\nCategory: {task.Category}\nPriority: {task.Priority}\nDue Date: {task.DueDate.ToShortDateString()}";
                            break;
                    }

                    mail.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        smtp.EnableSsl = true; // Set to true if your SMTP server requires SSL
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email: {ex.Message}");
            }
        }


        private void InitializeCategories()
        {
            comboBoxCategory.Items.AddRange(new string[] { "Work", "Personal", "Shopping", "Other" });
            comboBoxCategory.SelectedIndex = 0;
        }

        private void InitializePriorities()
        {


            comboBoxPriority.Items.AddRange(new string[] { "Low", "Medium", "High" });
            comboBoxPriority.SelectedIndex = -1; // Default selection
        }






        public class Task
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public string Priority { get; set; }
            public DateTime DueDate { get; set; }

            public override string ToString()
            {
                return $"{Name} - {Category} - {Priority} - Due: {DueDate.ToShortDateString()}";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (comboBoxPriority.SelectedIndex == -1)
            {
                MessageBox.Show("Select a priority level first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var task = new Task
                {
                    Name = txtTaskName.Text,
                    Category = comboBoxCategory.SelectedItem.ToString(),
                    Priority = comboBoxPriority.SelectedItem.ToString(),
                    DueDate = dateTimePickerDueDate.Value
                };

                tasks.Add(task);
                RefreshTaskList();
                ClearInputs();

                // Send an email reminder if the task is due within 1 day
                if ((task.DueDate - DateTime.Now).TotalDays <= 1)
                {
                    SendEmailReminder(task);
                }
            }


        }
        private void ClearInputs()
        {
            txtTaskName.Clear();
            comboBoxCategory.SelectedIndex = 0;
            comboBoxPriority.SelectedIndex = 1;
            dateTimePickerDueDate.Value = DateTime.Now;
        }

        private void btnEditTask_Click(object sender, EventArgs e)
        {


            if (txtTaskName.Text == "")
            {
                MessageBox.Show("You Cannot Edit Before Adding To-Do List Modified", "Information",
    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            else
            {

                if (listViewTasks.SelectedIndices.Count > 0)
                {
                    int index = listViewTasks.SelectedIndices[0];

                    // Update the task
                    tasks[index].Name = txtTaskName.Text;
                    tasks[index].Category = comboBoxCategory.SelectedItem.ToString();
                    tasks[index].Priority = comboBoxPriority.SelectedItem.ToString();
                    tasks[index].DueDate = dateTimePickerDueDate.Value;

                    // Verify that the task is being updated correctly
                    MessageBox.Show($"Edited Task: {tasks[index].Name} - {tasks[index].Category} - {tasks[index].Priority} - Due: {tasks[index].DueDate.ToShortDateString()}");

                    // Refresh the ListView
                    RefreshTaskList();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Please select a task to edit.");
                }
            }


        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedIndices.Count > 0)
            {
                int index = listViewTasks.SelectedIndices[0];
                tasks.RemoveAt(index);
                RefreshTaskList();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Please select a task to delete.");
            }
        }

        private void RefreshTaskList()
        {
            listViewTasks.Items.Clear(); // Clear existing items

            // Create groups for priorities
            ListViewGroup lowGroup = new ListViewGroup("Low Priority", HorizontalAlignment.Left);
            ListViewGroup mediumGroup = new ListViewGroup("Medium Priority", HorizontalAlignment.Left);
            ListViewGroup highGroup = new ListViewGroup("High Priority", HorizontalAlignment.Left);

            listViewTasks.Groups.Add(lowGroup);
            listViewTasks.Groups.Add(mediumGroup);
            listViewTasks.Groups.Add(highGroup);

            foreach (var task in tasks)
            {
                var listViewItem = new ListViewItem(task.Name);
                listViewItem.SubItems.Add(task.Category);
                listViewItem.SubItems.Add(task.Priority);
                listViewItem.SubItems.Add(task.DueDate.ToShortDateString());

                // Assign group based on priority
                switch (task.Priority)
                {
                    case "Low":
                        listViewItem.Group = lowGroup;
                        listViewItem.ForeColor = Color.Green;
                        break;
                    case "Medium":
                        listViewItem.Group = mediumGroup;
                        listViewItem.ForeColor = Color.Orange;
                        break;
                    case "High":
                        listViewItem.Group = highGroup;
                        listViewItem.ForeColor = Color.Red;
                        break;
                    default:
                        listViewItem.ForeColor = Color.Black;
                        break;
                }

                // Add the item to the ListView
                listViewTasks.Items.Add(listViewItem);
            }
        }


        private void comboBoxPriority_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        private void ReminderTimer_Tick(object sender, EventArgs e)
        {

            foreach (var task in tasks)
            {
                if ((task.DueDate - DateTime.Now).TotalDays <= 1)
                {
                    SendEmailReminder(task);
                }
            }

        }

        private void lowPriorityTimer_Tick(object sender, EventArgs e)
        {
            SendRemindersForPriority("Low");
        }

        private void mediumPriorityTimer_Tick(object sender, EventArgs e)
        {
            SendRemindersForPriority("Medium");


        }

        private void highPriorityTimer_Tick(object sender, EventArgs e)
        {
            SendRemindersForPriority("High");
        }


        private void SendRemindersForPriority(string priority)
        {
            foreach (var task in tasks)
            {
                if (task.Priority == priority && (task.DueDate - DateTime.Now).TotalDays <= 1)
                {
                    SendEmailReminder(task);
                }
            }
        }

        public class ListViewItemComparer : System.Collections.IComparer
        {
            private int column;
            private SortOrder sortOrder;

            public ListViewItemComparer(int column, SortOrder sortOrder)
            {
                this.column = column;
                this.sortOrder = sortOrder;
            }

            public int Compare(object x, object y)
            {
                ListViewItem item1 = (ListViewItem)x;
                ListViewItem item2 = (ListViewItem)y;

                int result = String.Compare(item1.SubItems[column].Text, item2.SubItems[column].Text);

                if (sortOrder == SortOrder.Descending)
                {
                    result = -result;
                }

                return result;
            }
        }

        private void listViewTasks_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if the column is already sorted.
            SortOrder sortOrder = listViewTasks.Sorting;
            if (listViewTasks.Sorting == SortOrder.Ascending)
            {
                sortOrder = SortOrder.Descending;
            }
            else
            {
                sortOrder = SortOrder.Ascending;
            }

            // Set the ListViewItemSorter
            listViewTasks.ListViewItemSorter = new ListViewItemComparer(e.Column, sortOrder);
            listViewTasks.Sorting = sortOrder;
        }





    }
}
