namespace To_Do_List_Application
{
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
        protected override void Dispose(bool disposing)
        {
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
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtTaskName = new TextBox();
            comboBoxCategory = new ComboBox();
            comboBoxPriority = new ComboBox();
            dateTimePickerDueDate = new DateTimePicker();
            btnAddTask = new Button();
            btnEditTask = new Button();
            btnDeleteTask = new Button();
            listViewTasks = new ListView();
            ReminderTimer = new System.Windows.Forms.Timer(components);
            lowPriorityTimer = new System.Windows.Forms.Timer(components);
            mediumPriorityTimer = new System.Windows.Forms.Timer(components);
            highPriorityTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // txtTaskName
            // 
            txtTaskName.Location = new Point(12, 47);
            txtTaskName.Name = "txtTaskName";
            txtTaskName.Size = new Size(776, 23);
            txtTaskName.TabIndex = 0;
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new Point(26, 125);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new Size(264, 23);
            comboBoxCategory.TabIndex = 1;
            // 
            // comboBoxPriority
            // 
            comboBoxPriority.FormattingEnabled = true;
            comboBoxPriority.Location = new Point(26, 169);
            comboBoxPriority.Name = "comboBoxPriority";
            comboBoxPriority.Size = new Size(264, 23);
            comboBoxPriority.TabIndex = 2;
            comboBoxPriority.SelectedIndexChanged += comboBoxPriority_SelectedIndexChanged;
            // 
            // dateTimePickerDueDate
            // 
            dateTimePickerDueDate.DropDownAlign = LeftRightAlignment.Right;
            dateTimePickerDueDate.Location = new Point(26, 215);
            dateTimePickerDueDate.Name = "dateTimePickerDueDate";
            dateTimePickerDueDate.Size = new Size(264, 23);
            dateTimePickerDueDate.TabIndex = 3;
            // 
            // btnAddTask
            // 
            btnAddTask.Location = new Point(26, 269);
            btnAddTask.Name = "btnAddTask";
            btnAddTask.Size = new Size(264, 43);
            btnAddTask.TabIndex = 4;
            btnAddTask.Text = "ADD";
            btnAddTask.UseVisualStyleBackColor = true;
            btnAddTask.Click += btnAddTask_Click;
            // 
            // btnEditTask
            // 
            btnEditTask.Location = new Point(26, 335);
            btnEditTask.Name = "btnEditTask";
            btnEditTask.Size = new Size(264, 43);
            btnEditTask.TabIndex = 5;
            btnEditTask.Text = "Edit";
            btnEditTask.UseVisualStyleBackColor = true;
            btnEditTask.Click += btnEditTask_Click;
            // 
            // btnDeleteTask
            // 
            btnDeleteTask.Location = new Point(26, 402);
            btnDeleteTask.Name = "btnDeleteTask";
            btnDeleteTask.Size = new Size(264, 43);
            btnDeleteTask.TabIndex = 6;
            btnDeleteTask.Text = "Delete";
            btnDeleteTask.UseVisualStyleBackColor = true;
            btnDeleteTask.Click += btnDeleteTask_Click;
            // 
            // listViewTasks
            // 
            listViewTasks.Location = new Point(335, 96);
            listViewTasks.Name = "listViewTasks";
            listViewTasks.Size = new Size(453, 342);
            listViewTasks.TabIndex = 7;
            listViewTasks.UseCompatibleStateImageBehavior = false;
            // 
            // ReminderTimer
            // 
            ReminderTimer.Tick += ReminderTimer_Tick;
            // 
            // lowPriorityTimer
            // 
            lowPriorityTimer.Tick += lowPriorityTimer_Tick;
            // 
            // mediumPriorityTimer
            // 
            mediumPriorityTimer.Tick += mediumPriorityTimer_Tick;
            // 
            // highPriorityTimer
            // 
            highPriorityTimer.Tick += highPriorityTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listViewTasks);
            Controls.Add(btnDeleteTask);
            Controls.Add(btnEditTask);
            Controls.Add(btnAddTask);
            Controls.Add(dateTimePickerDueDate);
            Controls.Add(comboBoxPriority);
            Controls.Add(comboBoxCategory);
            Controls.Add(txtTaskName);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "To-Do List Application - Eng Maged Ali";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTaskName;
        private ComboBox comboBoxCategory;
        private ComboBox comboBoxPriority;
        private DateTimePicker dateTimePickerDueDate;
        private Button btnAddTask;
        private Button btnEditTask;
        private Button btnDeleteTask;
        private ListView listViewTasks;
        private System.Windows.Forms.Timer ReminderTimer;
        
    }
}
