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
    
    public partial class Object: BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Object()
        {
            this.InputInfoes = new HashSet<InputInfo>();
            this.OutputInfoes = new HashSet<OutputInfo>();
        }

        public string Id { get => _id; set { _id = value; OnPropertyChanged(); } }
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }
        public int IdUnit { get => _idUnit; set { _idUnit = value; OnPropertyChanged();  } }
        public string QrCode { get => _qrCode; set { _qrCode = value; OnPropertyChanged(); } }
        public string BarCode { get => _barCode; set { _barCode = value; OnPropertyChanged(); } }
        public int IdSupplier { get => _idSupplier; set { _idSupplier = value; OnPropertyChanged(); } }
        public double? OutputPrice { get => _outputPrice; set { _outputPrice = value; OnPropertyChanged(); } }

        private string _id;
        private string _displayName;
        private int _idUnit;
        private string _qrCode;
        private string _barCode;
        private int _idSupplier;
        private Nullable<double> _outputPrice;


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InputInfo> InputInfoes { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Unit Unit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutputInfo> OutputInfoes { get; set; }

    }
}
