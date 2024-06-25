<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128635953/15.2.9%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T372123)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# WinForms Scheduler - Replace a default Scheduler command with a custom command

The WinForms Scheduler Control exposes the [ISchedulerCommandFactoryService](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraScheduler.Services.ISchedulerCommandFactoryService) interface that enables you to substitute the default command with your own (custom) command. This example replaces the default **SplitAppointmentOperationCommand** with a custom command. The custom command has the `SplitAppointmentCommandStep` property that specifies a fixed split operation's step.

```csharp
public class CustomSplitAppointmentOperationCommand : SplitAppointmentOperationCommand {
    public CustomSplitAppointmentOperationCommand(ISchedulerCommandTarget target) : base(target) { }
    public CustomSplitAppointmentOperationCommand(SchedulerControl control) : base(control) { }
    private TimeSpan splitInterval = TimeSpan.FromMinutes(10);
    public TimeSpan SplitAppointmentCommandStep {
        get {
            return splitInterval;
        }
        set {
            splitInterval = value;
        }
    }
    protected override IOperation CreateOperation() {
        TimeScaleCollection timeScales = new TimeScaleCollection();
        timeScales.Add(new TimeScaleFixedInterval(SplitAppointmentCommandStep));
        return new SplitAppointmentOperation(SchedulerControl, timeScales, SchedulerControl.SelectedAppointments[0]);
    }
}
```


## Implementation Details

* Create a custom command class inherited from the command that you wish to replace.
* Override required methods of the command.
* Create a class that implements the `ISchedulerCommandFactoryService` interface. In this class, you need to implement the `CreateCommand` method to instantiate a custom command class. An identifier of the currently processed command is passed as a parameter of this method.
* Use the custom class that implements the `ISchedulerCommandFactoryService` interface to substitute the default command service. In this case, the Scheduler control will use the custom command instead of the default command.


## Files to Review

* [CustomSchedulerCommandFactoryService.cs](./CS/WindowsFormsApplication1/CustomSchedulerCommandFactoryService.cs) (VB: [CustomSchedulerCommandFactoryService.vb](./VB/WindowsFormsApplication1/CustomSchedulerCommandFactoryService.vb))
* [Form1.cs](./CS/WindowsFormsApplication1/Form1.cs) (VB: [Form1.vb](./VB/WindowsFormsApplication1/Form1.vb))


## Documentation

* [Services](https://docs.devexpress.com/WindowsForms/4106/controls-and-libraries/scheduler/services)
* [Command Factory Service](https://docs.devexpress.com/WindowsForms/120381/controls-and-libraries/scheduler/services/command-factory-service)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-replace-default-command&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-replace-default-command&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
