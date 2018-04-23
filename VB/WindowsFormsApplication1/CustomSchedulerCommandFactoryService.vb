Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Commands
Imports DevExpress.XtraScheduler.Operations
Imports DevExpress.XtraScheduler.Native
Imports DevExpress.XtraScheduler.Services
Imports DevExpress.Utils

Namespace WindowsFormsApplication1
	Public Class CustomSchedulerCommandFactoryService
		Implements ISchedulerCommandFactoryService
		Private ReadOnly service As ISchedulerCommandFactoryService
		Private ReadOnly control As SchedulerControl

		Public Sub New(ByVal control As SchedulerControl, ByVal service As ISchedulerCommandFactoryService)
			Guard.ArgumentNotNull(control, "control")
			Guard.ArgumentNotNull(service, "service")
			Me.control = control
			Me.service = service
		End Sub

		#Region "ISchedulerCommandFactoryService Members"
        Public Function CreateCommand(ByVal id As SchedulerCommandId) As SchedulerCommand Implements ISchedulerCommandFactoryService.CreateCommand
            If id = SchedulerCommandId.SplitAppointment Then
                Dim cmd As New CustomSplitAppointmentOperationCommand(control)
                cmd.SplitAppointmentCommandStep = TimeSpan.FromHours(12)
                Return cmd
            End If
            Return service.CreateCommand(id)
        End Function
		#End Region
	End Class

	Public Class CustomSplitAppointmentOperationCommand
		Inherits SplitAppointmentOperationCommand
		Public Sub New(ByVal target As ISchedulerCommandTarget)
			MyBase.New(target)
		End Sub
		Public Sub New(ByVal control As SchedulerControl)
			MyBase.New(control)
		End Sub

		Private splitInterval As TimeSpan = TimeSpan.FromMinutes(10)
		Public Property SplitAppointmentCommandStep() As TimeSpan
			Get
				Return splitInterval
			End Get
			Set(ByVal value As TimeSpan)
				splitInterval = value
			End Set
		End Property
		Protected Overrides Function CreateOperation() As IOperation
			'TimeScaleCollection timeScales = CreateTimeScaleCollection();
			Dim timeScales As New TimeScaleCollection()
			timeScales.Add(New TimeScaleFixedInterval(SplitAppointmentCommandStep))
			Return New SplitAppointmentOperation(SchedulerControl, timeScales, SchedulerControl.SelectedAppointments(0))
		End Function
	End Class
End Namespace
