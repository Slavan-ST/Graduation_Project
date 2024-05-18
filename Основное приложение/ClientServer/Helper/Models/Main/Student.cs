using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;

namespace Helper.Models.Main
{
    public class Student: Base
    {
        public Student() 
        {

        }
        [Reactive]
        public string Name { get; set; } = string.Empty;
        [Reactive]
        public string Surname { get; set; } = string.Empty;
        [Reactive]
        public string Patronymic { get; set; } = string.Empty;
        [Reactive]
        public DateTime DateBirthday { get; set; }

        [Reactive]
        public string Phone { get; set; } = string.Empty;
        [Reactive]
        public string Address { get; set; } = string.Empty;
        [Reactive]
        public string RepresentativeName { get; set; } = string.Empty;
        [Reactive]
        public string RepresentativeSurname { get; set; } = string.Empty;
        [Reactive]
        public string RepresentativePatronymic { get; set; } = string.Empty;
        [Reactive]
        public string RepresentativePhone { get; set; } = string.Empty;
        [Reactive]
        public string Gender { get; set; } = string.Empty; //1 символ


        [Reactive]
        public int RoomId { get; set; }
        [Reactive]
        public int GroupId { get; set; }
        [Reactive]
        public int StatusId { get; set; }



        IEnumerable<AttendanceLog>? _attendanceLogs = new List<AttendanceLog>();
        public IEnumerable<AttendanceLog>? AttendanceLogs
        {
            get
            {
                if (_attendanceLogs != null)
                {
                    foreach (var log in _attendanceLogs)
                    {
                        log.Student = null;
                    }
                }
                return _attendanceLogs;
            }
            set
            {
                _attendanceLogs = value;
            }
        }
        IEnumerable<DutySchedule>? _dutySchedules = new List<DutySchedule>();
        public IEnumerable<DutySchedule>? DutySchedules
        {
            get
            {
                if (_dutySchedules != null)
                {
                    foreach (var log in _dutySchedules)
                    {
                        log.Student = null;
                    }
                }
                return _dutySchedules;
            }
            set
            {
                _dutySchedules = value;
            }
        }

        Room? _room;
        public Room? Room
        {
            get
            {
                if (_room == null)
                {
                    return null;
                }
                _room.Students = null;
                return _room;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _room, value);
            }
        }
        Status? _status;
        public Status? Status
        {
            get
            {
                if (_status == null)
                {
                    return null;
                }
                _status.Students = null;
                return _status;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _status, value);
            }
        }
        Group? _group;
        public Group? Group
        {
            get
            {
                if (_group == null)
                {
                    return null;
                }
                _group.Students = null;
                return _group;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _group, value);
            }
        }
        public string FIO
        {
            get => $"{Surname} {Name} {Patronymic}";
        }
        public int Age
        {
            get
            {
                return (DateTime.Now - DateBirthday).Hours / (24 * 365);
            }
        }

        public string ShortDateBirthday
        {
            get => DateBirthday.ToShortDateString();
        }

        public string RepresentativeFIO
        {
            get => $"{RepresentativeName} {RepresentativePatronymic} {RepresentativeSurname}";
        }
        public override string ToString()
        {
            return FIO;
        }
    }
}
