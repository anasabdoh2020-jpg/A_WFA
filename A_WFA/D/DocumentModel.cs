using System;

namespace A_WFA.D
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DocumentTypeId { get; set; }
        public int CategoryId { get; set; }
        public int FromDepartmentId { get; set; }
        public int ToDepartmentId { get; set; }
        public int BoxId { get; set; }
        public string DocumentDate { get; set; }
        public string ReceiveDate { get; set; }
        public string IssueDate { get; set; }
        public int UploadedBy { get; set; }
        public string UploadedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string DocumentNature { get; set; }
        public string Summary { get; set; }
        public string Notes { get; set; }
        public string ArchiveDocNumber { get; set; }
        public string ReferenceNumber { get; set; }

        // ✅ حقول الملف الجديدة (المضافة)
        public string FilePath { get; set; }      // المسار النسبي للملف المشفر
        public string FileName { get; set; }      // اسم الملف الأصلي
        public string FileType { get; set; }      // نوع الملف
        public long FileSize { get; set; }        // حجم الملف
        public string FileHash { get; set; }      // Hash للتحقق من السلامة
        public byte[] FileData { get; set; }      // بيانات الملف (للاستخدام المؤقت)
        public bool IsActive { get; set; } = true;

        // حقول إضافية (اختيارية)
        public string CreatedAt { get; set; }
        public string UpdatedAt2 { get; set; }
    }
}