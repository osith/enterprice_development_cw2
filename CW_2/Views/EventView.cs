using CW_2.Models;
using System;
using System.Windows.Forms;
using static CW_2.Models.Enums;
using static CW_2.Models.EventModel;
using static CW_2.Models.UserModal;

namespace CW_2.Views
{
    public partial class EventView : Form
    {
        private readonly RegisterUserDTO _loggedUser;
        private readonly EventModel _eventModel;
        private Guid updateEventId;
        public EventView(RegisterUserDTO user)
        {
            _loggedUser = user;
            _eventModel = new EventModel();
            InitializeComponent();
        }

        private void EventView_Load(object sender, EventArgs e)
        {
            loadEvents();
        }

        private void loadEvents()
        {
            var events = _eventModel.LoadAllEvents(_loggedUser.Id);
            if (events.Count > 0)
            {
                eventTabel.DataSource = events;
                eventTabel.Columns["Id"].Visible = false;
                eventTabel.Columns["UserDataId"].Visible = false;
                eventTabel.Columns["CreatedAt"].Visible = false;
                eventTabel.Columns["IsDeleted"].Visible = false;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {

            if (cmbType.SelectedItem == null || string.IsNullOrEmpty(txtDescription.Text))
            {
                Helper.DisplayMessage("Please fill all fileds", MessageType.Warning);
                return;
            }

            if (chkIsRecurring.Checked)
            {
                if (cmbRecurringType.SelectedItem == null)
                {
                    Helper.DisplayMessage("Please select recurring type", MessageType.Warning);
                    return;
                }
            }

            var type = cmbType.SelectedItem.ToString();
            if (type == EventTypeString.APPOINTMENT)
            {
                var appointment = new AppointmentDTO()
                {
                    Id = Guid.NewGuid(),
                    Description = txtDescription.Text,
                    Type = (short)EventType.Appointment,
                    Recurring = chkIsRecurring.Checked ? true : false,
                    RecurringType = chkIsRecurring.Checked == true ? _eventModel.getRecurringTypeEnum(cmbRecurringType.SelectedItem.ToString()) : null,
                    UserDataId = _loggedUser.Id,
                    CreatedAt = DateTime.UtcNow,
                    EventAt = txtPlace.Text,
                    EventOn = dtpDateTime.Value
                };
                await _eventModel.SaveEvent(appointment);
                Helper.DisplayMessage("Appointment added", MessageType.Complete);
            }
            else if (type == EventTypeString.TASK)
            {
                var task = new TaskDTO()
                {
                    Id = Guid.NewGuid(),
                    Description = txtDescription.Text,
                    Type = (short)EventType.Task,
                    Recurring = chkIsRecurring.Checked ? true : false,
                    RecurringType = chkIsRecurring.Checked == true ? _eventModel.getRecurringTypeEnum(cmbRecurringType.SelectedItem.ToString()) : null,
                    UserDataId = _loggedUser.Id,
                    CreatedAt = DateTime.UtcNow,
                    Complete = dtpDateTime.Value
                };
                await _eventModel.SaveEvent(task);
                Helper.DisplayMessage("Event added", MessageType.Complete);
            }
            loadEvents();
        }

        private void EventView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var next = new HomeNavView(_loggedUser);
            next.Show();
            this.Dispose();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var select = ((ComboBox)sender).SelectedItem.ToString();
            if (select == EventTypeString.APPOINTMENT)
            {
                lblDynDate.Text = "On";
                lblDynDate.Visible = true;
                dtpDateTime.Visible = true;
                lblPlace.Visible = true;
                txtPlace.Visible = true;
            }
            else if (select == EventTypeString.TASK)
            {
                lblDynDate.Text = "Complete Before";
                lblDynDate.Visible = true;
                dtpDateTime.Visible = true;
                lblPlace.Visible = false;
                txtPlace.Visible = false;
            }
        }

        private void chkIsRecurring_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsRecurring.Checked)
                cmbRecurringType.Visible = true;
            else
                cmbRecurringType.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selected = (EventViewDTO)eventTabel.SelectedRows[0].DataBoundItem;
            if (selected != null)
            {
                updateEventId = selected.Id;
                txtDescription.Text = selected.Description;
                cmbType.SelectedItem = selected.Type;
                chkIsRecurring.Checked = selected.Recurring == true ? true : false;
                cmbRecurringType.SelectedItem = selected.Recurring == true ? selected.RecurringType : null;

                if (selected.Type == EventTypeString.APPOINTMENT)
                {
                    lblDynDate.Text = "On";
                    lblDynDate.Visible = true;
                    dtpDateTime.Visible = true;
                    lblPlace.Visible = true;
                    txtPlace.Visible = true;
                    txtPlace.Text = selected.EventAt;
                    dtpDateTime.Value = selected.EventOn.Value;
                }
                else if (selected.Type == EventTypeString.TASK)
                {
                    lblDynDate.Text = "Complete Before";
                    lblDynDate.Visible = true;
                    dtpDateTime.Visible = true;
                    lblPlace.Visible = false;
                    txtPlace.Visible = false;
                    dtpDateTime.Value = selected.CompleteBefore.Value;
                }


            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDescription.Text = string.Empty;
            cmbType.SelectedValue = null;
            chkIsRecurring.Checked = false;
            cmbRecurringType.SelectedValue = null;
            cmbRecurringType.Visible = false;
            dtpDateTime.Value = DateTime.UtcNow;
            dtpDateTime.Visible = false;
            lblDynDate.Visible = false;
            txtPlace.Text = string.Empty;
            txtPlace.Visible = false;
            lblPlace.Visible = false;
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbType.SelectedItem == null || string.IsNullOrEmpty(txtDescription.Text))
            {
                Helper.DisplayMessage("Please fill all fileds", MessageType.Warning);
                return;
            }

            if (chkIsRecurring.Checked)
            {
                if (cmbRecurringType.SelectedItem == null)
                {
                    Helper.DisplayMessage("Please select recurring type", MessageType.Warning);
                    return;
                }
            }

            var type = cmbType.SelectedItem.ToString();
            if (type == EventTypeString.APPOINTMENT)
            {
                var appointment = new AppointmentDTO()
                {
                    Id = updateEventId,
                    Description = txtDescription.Text,
                    Type = (short)EventType.Appointment,
                    Recurring = chkIsRecurring.Checked ? true : false,
                    RecurringType = chkIsRecurring.Checked == true ? _eventModel.getRecurringTypeEnum(cmbRecurringType.SelectedItem.ToString()) : null,
                    UserDataId = _loggedUser.Id,
                    EventAt = txtPlace.Text,
                    EventOn = dtpDateTime.Value
                };
                await _eventModel.UpdateEvent(appointment);
                Helper.DisplayMessage("Appointment updated", MessageType.Complete);
            }
            else if (type == EventTypeString.TASK)
            {
                var task = new TaskDTO()
                {
                    Id = updateEventId,
                    Description = txtDescription.Text,
                    Type = (short)EventType.Task,
                    Recurring = chkIsRecurring.Checked ? true : false,
                    RecurringType = chkIsRecurring.Checked == true ? _eventModel.getRecurringTypeEnum(cmbRecurringType.SelectedItem.ToString()) : null,
                    UserDataId = _loggedUser.Id,
                    Complete = dtpDateTime.Value
                };
                await _eventModel.UpdateEvent(task);
                Helper.DisplayMessage("Task updated", MessageType.Complete);
            }
            loadEvents();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = (EventViewDTO)eventTabel.SelectedRows[0].DataBoundItem;
            if (Helper.ConfirmMessage("Do you need to delete selected event in table?"))
            {
                if (selected.Id != Guid.Empty)
                {
                    if (await _eventModel.DeleteEvent(selected.Id))
                        Helper.DisplayMessage("Event deleted", MessageType.Complete);
                    else
                        Helper.DisplayMessage("Event not deleted", MessageType.Error);
                }
                else
                    Helper.DisplayMessage("Please select event to delete", MessageType.Warning);
                loadEvents();
            }
        }
    }
}
