//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKho.Model
{
    using QuanLyKho.ViewModel;
    using System;
    using System.Collections.Generic;

    public partial class Object : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Object()
        {
            this.InputInfoes = new HashSet<InputInfo>();
            this.OutputInfoes = new HashSet<OutputInfo>();
        }

        private string _id { get; set; }
        public string Id { get => _id; set { _id = value; OnPropertyChanged(); } }

        private string _displayName { get; set; }
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }

        private int _idUnit { get; set; }
        public int IdUnit { get => _idUnit; set { _idUnit = value; OnPropertyChanged(); } }

        private int _idSupplier { get; set; }
        public int IdSupplier { get => _idSupplier; set { _idSupplier = value; OnPropertyChanged(); } }
        
        private string _qrCode { get; set; }
        public string QrCode { get => _qrCode; set { _qrCode = value; OnPropertyChanged(); } }
        
        private string _barCode { get; set; }
        public string BarCode { get => _barCode; set { _barCode = value; OnPropertyChanged(); } }
        
        private Nullable<double> _outputPrice { get; set; }
        public Nullable<double> OutputPrice { get => _outputPrice; set { _outputPrice= value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InputInfo> InputInfoes { get; set; }

        private Supplier _supplier { get; set; }
        public virtual Supplier Supplier { get => _supplier; set { _supplier = value; OnPropertyChanged(); } }
        

        private Unit _unit { get; set; }
        public virtual Unit Unit { get => _unit; set { _unit = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutputInfo> OutputInfoes { get; set; }
    }
}
