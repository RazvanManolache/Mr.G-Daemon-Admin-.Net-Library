using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Data
{

    public class SubApplication : ObservableObject
    {
        [JsonIgnore]
        public StringBuffer ConsoleBuffer { get; set; } = new StringBuffer();
        [JsonIgnore]
        public StringBuffer LogBuffer { get; set; } = new StringBuffer();


        private bool controlVisible = true;
        [JsonIgnore]
        public bool ControlVisible
        {
            get { return controlVisible; }
            set { SetProperty(ref controlVisible, value); }
        }

        private bool settingsVisible;
        [JsonIgnore]
        public bool SettingsVisible
        {
            get { return settingsVisible; }
            set { SetProperty(ref settingsVisible, value); }
        }

        private bool runOptionsVisible;
        [JsonIgnore]
        public bool RunOptionsVisible
        {
            get { return runOptionsVisible; }
            set { SetProperty(ref runOptionsVisible, value); }
        }
        private bool commandVisible;
        [JsonIgnore]
        public bool CommandVisible
        {
            get { return commandVisible; }
            set { SetProperty(ref commandVisible, value); }
        }
        private bool sourceVisible;
        [JsonIgnore]
        public bool SourceVisible
        {
            get { return sourceVisible; }
            set { SetProperty(ref sourceVisible, value); }
        }
        private bool consoleVisible;
        [JsonIgnore]
        public bool ConsoleVisible
        {
            get { return consoleVisible; }
            set { SetProperty(ref consoleVisible, value); }
        }

        [JsonIgnore]
        public bool StartEnabled
        {
            get { return !Running; }
        }

        [JsonIgnore]
        public bool StopEnabled
        {
            get { return Running; }
        }

        [JsonIgnore]
        public bool RestartEnabled
        {
            get { return Running; }
        }

       
        [JsonIgnore]
        public string NotificationText
        {
            get {
                var text = "";
                if(this.Status!="Running" )
                {
                    text = $"Application is {this.Status}. ";
                }
                if(this.HasUpdates)
                {
                    text += "There are updates available. ";
                }
                return text; 
            }
           
        }

      
        [JsonIgnore]
        public bool NotificationVisible
        {
            get { return !string.IsNullOrWhiteSpace( NotificationText); }
          
        }




        private string id = "";
        public string Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string name = "";
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private bool hasUpdates = false;

        public bool HasUpdates
        {
            get { return hasUpdates; }
            set { SetProperty(ref hasUpdates, value); 
                this.OnPropertyChanged(nameof(NotificationText));
                this.OnPropertyChanged(nameof(NotificationVisible));
            }
        }
        private string commandExec = "";
        public string CommandExec
        {
            get { return commandExec; }
            set { SetProperty(ref commandExec, value); }
        }

        private string command = "";
        public string Command
        {
            get { return command; }
            set { SetProperty(ref command, value); }
        }

        private bool restartOnCriticalError;
        public bool RestartOnCriticalError
        {
            get { return restartOnCriticalError; }
            set { SetProperty(ref restartOnCriticalError, value); }
        }

        private ObservableCollection<string> criticalErrorMessages = new ObservableCollection<string>();
        public ObservableCollection<string> CriticalErrorMessages
        {
            get { return criticalErrorMessages; }
            set { SetProperty(ref criticalErrorMessages, value); }
        }

        public string CriticalErrorMessagesString
        {
            set
            {
                CriticalErrorMessages.Clear();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    CriticalErrorMessages = new ObservableCollection<string>(value.Split('\n'));
                }
            }
            get
            {
                return string.Join("\n", CriticalErrorMessages);
            }
        }

        private bool autoStart;
        public bool AutoStart
        {
            get { return autoStart; }
            set { SetProperty(ref autoStart, value); }
        }

        private string repoURL = "";
        public string RepoURL
        {
            get { return repoURL; }
            set { SetProperty(ref repoURL, value); }
        }

        private string branch = "main";
        public string Branch
        {
            get { return branch; }
            set { SetProperty(ref branch, value); }
        }

        private string path = "";
        public string Path
        {
            get { return path; }
            set { SetProperty(ref path, value); }
        }

        private bool autoUpdate;
        public bool AutoUpdate
        {
            get { return autoUpdate; }
            set { SetProperty(ref autoUpdate, value); }
        }

        private ObservableCollection<string> flags = new ObservableCollection<string>();
        public ObservableCollection<string> Flags
        {
            get { return flags; }
            set { SetProperty(ref flags, value); }
        }

        public string FlagsString
        {
            set
            {
                Flags.Clear();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Flags = new ObservableCollection<string>(value.Split('\n'));
                }
            }
            get
            {
                return string.Join("\n", Flags);
            }
        }

        private string appType = "";
        public string AppType
        {
            get { return appType; }
            set { SetProperty(ref appType, value); }
        }

        private string status = "";
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); 
                this.OnPropertyChanged(nameof(NotificationText));
                this.OnPropertyChanged(nameof(NotificationVisible));
            }
        }

        private string setupCommand = "";
        public string SetupCommand
        {
            get { return setupCommand; }
            set
            {
                SetProperty(ref setupCommand, value);
            }
        }

        private bool firstRun;
        public bool FirstRun
        {
            get { return firstRun; }
            set { SetProperty(ref firstRun, value); }
        }

     

        private bool running;
        public bool Running
        {
            get { return running; }
            set { 
                SetProperty(ref running, value); 
                this.OnPropertyChanged(nameof(StartEnabled));
                this.OnPropertyChanged(nameof(StopEnabled));
                this.OnPropertyChanged(nameof(RestartEnabled));

            }
        }

        public SubApplication Update(SubApplication subApplication)
        {
            Name = subApplication.Name;
            Command = subApplication.Command;
            RestartOnCriticalError = subApplication.RestartOnCriticalError;
            CriticalErrorMessages = subApplication.CriticalErrorMessages;
            AutoStart = subApplication.AutoStart;
            RepoURL = subApplication.RepoURL;
            Branch = subApplication.Branch;
            Path = subApplication.Path;
            HasUpdates = subApplication.HasUpdates;
            AutoUpdate = subApplication.AutoUpdate;
            Flags = subApplication.Flags;
            AppType = subApplication.AppType;
            Status = subApplication.Status;
            FirstRun = subApplication.FirstRun;
            Running = subApplication.Running;
            this.OnPropertyChanged(nameof(CriticalErrorMessagesString));
            this.OnPropertyChanged(nameof(FlagsString));
          
            return this;
        }

        private bool logVisible;

        public bool LogVisible { get => logVisible; set => SetProperty(ref logVisible, value); }
    }
}
