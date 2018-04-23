using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Commands;
using DevExpress.XtraScheduler.Operations;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraScheduler.Services;
using DevExpress.Utils;

namespace WindowsFormsApplication1 {
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1() {
            InitializeComponent();
        }
        public static Random RandomInstance = new Random();

        private BindingList<CustomResource> CustomResourceCollection = new BindingList<CustomResource>();
        private BindingList<CustomAppointment> CustomEventList = new BindingList<CustomAppointment>();

        private void Form1_Load(object sender, EventArgs e) {
            InitResources();
            InitAppointments();
            CustomSchedulerCommandFactoryService commandFactory =
            new CustomSchedulerCommandFactoryService(schedulerControl1, schedulerControl1.GetService<ISchedulerCommandFactoryService>());
            schedulerControl1.RemoveService(typeof(ISchedulerCommandFactoryService));
            schedulerControl1.AddService(typeof(ISchedulerCommandFactoryService), commandFactory);

            schedulerControl1.Start = DateTime.Now;
            schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
            schedulerControl1.TimelineView.AppointmentDisplayOptions.SnapToCellsMode = AppointmentSnapToCellsMode.Never;
        }

        private void InitResources() {
            ResourceMappingInfo mappings = this.schedulerStorage1.Resources.Mappings;
            mappings.Id = "ResID";
            mappings.Caption = "Name";

            CustomResourceCollection.Add(CreateCustomResource(1, "Max Fowler", Color.PowderBlue));
            CustomResourceCollection.Add(CreateCustomResource(2, "Nancy Drewmore", Color.PaleVioletRed));
            CustomResourceCollection.Add(CreateCustomResource(3, "Pak Jang", Color.PeachPuff));
            this.schedulerStorage1.Resources.DataSource = CustomResourceCollection;
        }

        private CustomResource CreateCustomResource(int res_id, string caption, Color ResColor) {
            CustomResource cr = new CustomResource();
            cr.ResID = res_id;
            cr.Name = caption;
            return cr;
        }



        private void InitAppointments() {
            AppointmentMappingInfo mappings = this.schedulerStorage1.Appointments.Mappings;
            mappings.Start = "StartTime";
            mappings.End = "EndTime";
            mappings.Subject = "Subject";
            mappings.AllDay = "AllDay";
            mappings.Description = "Description";
            mappings.Label = "Label";
            mappings.Location = "Location";
            mappings.RecurrenceInfo = "RecurrenceInfo";
            mappings.ReminderInfo = "ReminderInfo";
            mappings.ResourceId = "OwnerId";
            mappings.Status = "Status";
            mappings.Type = "EventType";

            GenerateEvents(CustomEventList);
            this.schedulerStorage1.Appointments.DataSource = CustomEventList;
        }


        private void GenerateEvents(BindingList<CustomAppointment> eventList) {
            int count = schedulerStorage1.Resources.Count;

            for(int i = 0; i < count; i++) {
                Resource resource = schedulerStorage1.Resources[i];
                string subjPrefix = resource.Caption + "'s ";
                eventList.Add(CreateEvent(subjPrefix + "meeting", resource.Id, 2, 5));
                eventList.Add(CreateEvent(subjPrefix + "travel", resource.Id, 3, 6));
                eventList.Add(CreateEvent(subjPrefix + "phone call", resource.Id, 0, 10));
            }
        }
        private CustomAppointment CreateEvent(string subject, object resourceId, int status, int label) {
            CustomAppointment apt = new CustomAppointment();
            apt.Subject = subject;
            apt.OwnerId = resourceId;
            apt.StartTime = DateTime.Today;
            apt.EndTime = apt.StartTime.AddDays(status + 1);
            apt.Status = status;
            apt.Label = label;
            return apt;
        }
    }
    
}
