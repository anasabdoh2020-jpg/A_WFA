using A_WFA.D;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_WFA.Database.LTE
{
    /// <summary>
    /// وحدة العمليات على قاعدة البيانات - CRUD للوثائق والبيانات
    /// </summary>
    public static class DatabaseModuleLite
    {
        #region "دوال تنفيذ الاستعلامات الأساسية"

        /// <summary>
        /// تنفيذ استعلام SQL وإرجاع DataTable
        /// </summary>
        public static DataTable ExecuteQuery(string sql, Dictionary<string, object> parameters = null)
        {
            return DatabaseManagerLite.ExecuteQuery(sql, parameters);
        }

        /// <summary>
        /// تنفيذ استعلام SQL (INSERT, UPDATE, DELETE)
        /// </summary>
        public static int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            return DatabaseManagerLite.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// تنفيذ استعلام SQL وإرجاع قيمة واحدة
        /// </summary>
        public static object ExecuteScalar(string sql, Dictionary<string, object> parameters = null)
        {
            return DatabaseManagerLite.ExecuteScalar(sql, parameters);
        }

        #endregion

        #region "دوال التصنيفات - Categories"

        /// <summary>
        /// الحصول على جميع التصنيفات
        /// </summary>
        public static DataTable GetAllCategories(bool activeOnly = false)
        {
            string query = "SELECT id, name, description, is_active FROM Document_Categories";
            if (activeOnly)
                query += " WHERE is_active = 1";
            query += " ORDER BY name";
            return ExecuteQuery(query);
        }

        /// <summary>
        /// الحصول على تصنيف بواسطة المعرف
        /// </summary>
        public static DataRow GetCategoryById(int id)
        {
            string query = "SELECT * FROM Document_Categories WHERE id = @id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            DataTable dt = ExecuteQuery(query, parameters);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        /// <summary>
        /// إضافة تصنيف جديد
        /// </summary>
        public static int AddCategory(string name, string description)
        {
            string query = @"
                INSERT INTO Document_Categories (name, description, is_active, created_at)
                VALUES (@name, @desc, 1, CURRENT_TIMESTAMP);
                SELECT last_insert_rowid();";

            var parameters = new Dictionary<string, object>
            {
                { "@name", name },
                { "@desc", description ?? (object)DBNull.Value }
            };

            object result = ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// تحديث تصنيف
        /// </summary>
        public static bool UpdateCategory(int id, string name, string description, bool isActive)
        {
            string query = @"
                UPDATE Document_Categories 
                SET name = @name, 
                    description = @desc, 
                    is_active = @isActive
updated_at = CURRENT_TIMESTAMP
                WHERE id = @id";

            var parameters = new Dictionary<string, object>
            {
                { "@id", id },
                { "@name", name },
                { "@desc", description ?? (object)DBNull.Value },
                { "@isActive", isActive ? 1 : 0 }
            };

            int result = ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        /// <summary>
        /// حذف تصنيف
        /// </summary>
        public static bool DeleteCategory(int id)
        {
            string query = "DELETE FROM Document_Categories WHERE id = @id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            int result = ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        #endregion

        #region "دوال الوثائق - Documents"

        /// <summary>
        /// حفظ وثيقة جديدة
        /// </summary>
        public static bool SaveDocument(DocumentModel document)
        {
            try
            {
                using (var conn = DatabaseManagerLite.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Documents (
                            title, document_type_id, category_id,
                            from_department_id, to_department_id, box_id,
                            document_date, receive_date, issue_date,
                            uploaded_by, status, priority, document_nature,
                            summary, notes, archiveDoc_number, ReferenceNumber,
                            file_path, file_name, file_type, file_size, file_hash,
                            is_active
                        ) VALUES (
                            @title, @docType, @category,
                            @fromDept, @toDept, @boxId,
                            @docDate, @recDate, @issueDate,
                            @userId, @status, @priority, @nature,
                            @summary, @notes, @archiveNo, @refNo,
                            @filePath, @fileName, @fileType, @fileSize, @fileHash,
                            1
                        );
                        SELECT last_insert_rowid();";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", document.Title ?? "");
                        cmd.Parameters.AddWithValue("@docType", document.DocumentTypeId);
                        cmd.Parameters.AddWithValue("@category", document.CategoryId);
                        cmd.Parameters.AddWithValue("@fromDept", document.FromDepartmentId);
                        cmd.Parameters.AddWithValue("@toDept", document.ToDepartmentId);
                        cmd.Parameters.AddWithValue("@boxId", document.BoxId);
                        cmd.Parameters.AddWithValue("@docDate", document.DocumentDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@recDate", document.ReceiveDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@issueDate", document.IssueDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@userId", document.UploadedBy);
                        cmd.Parameters.AddWithValue("@status", document.Status ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@priority", document.Priority ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@nature", document.DocumentNature ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@summary", document.Summary ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@notes", document.Notes ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@archiveNo", document.ArchiveDocNumber ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@refNo", document.ReferenceNumber ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@filePath", document.FilePath ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@fileName", document.FileName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@fileType", document.FileType ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@fileSize", document.FileSize);
                        cmd.Parameters.AddWithValue("@fileHash", document.FileHash ?? (object)DBNull.Value);

                        object result = cmd.ExecuteScalar();
                        document.Id = Convert.ToInt32(result);

                        DatabaseManagerLite.SafeLogAuditTrail(
                            document.UploadedBy,
                            "ADD_DOCUMENT",
                            $"تم إضافة وثيقة جديدة: {document.Title}"
                        );

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ خطأ في SaveDocument: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// تحديث وثيقة
        /// </summary>
        public static bool UpdateDocument(DocumentModel document)
        {
            try
            {
                using (var conn = DatabaseManagerLite.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        UPDATE Documents SET
                            title = @title,
                            document_type_id = @docType,
                            category_id = @category,
                            from_department_id = @fromDept,
                            to_department_id = @toDept,
                            box_id = @boxId,
                            document_date = @docDate,
                            receive_date = @recDate,
                            issue_date = @issueDate,
                            status = @status,
                            priority = @priority,
                            document_nature = @nature,
                            summary = @summary,
                            notes = @notes,
                            archiveDoc_number = @archiveNo,
                            ReferenceNumber = @refNo,
                            file_path = @filePath,
                            file_name = @fileName,
                            file_type = @fileType,
                            file_size = @fileSize,
                            file_hash = @fileHash,
                            updated_at = CURRENT_TIMESTAMP
                        WHERE id = @id";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", document.Id);
                        cmd.Parameters.AddWithValue("@title", document.Title ?? "");
                        cmd.Parameters.AddWithValue("@docType", document.DocumentTypeId);
                        cmd.Parameters.AddWithValue("@category", document.CategoryId);
                        cmd.Parameters.AddWithValue("@fromDept", document.FromDepartmentId);
                        cmd.Parameters.AddWithValue("@toDept", document.ToDepartmentId);
                        cmd.Parameters.AddWithValue("@boxId", document.BoxId);
                        cmd.Parameters.AddWithValue("@docDate", document.DocumentDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@recDate", document.ReceiveDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@issueDate", document.IssueDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@status", document.Status ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@priority", document.Priority ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@nature", document.DocumentNature ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@summary", document.Summary ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@notes", document.Notes ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@archiveNo", document.ArchiveDocNumber ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@refNo", document.ReferenceNumber ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@filePath", document.FilePath ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@fileName", document.FileName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@fileType", document.FileType ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@fileSize", document.FileSize);
                        cmd.Parameters.AddWithValue("@fileHash", document.FileHash ?? (object)DBNull.Value);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ خطأ في UpdateDocument: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// حذف وثيقة
        /// </summary>
        public static bool DeleteDocument(int documentId)
        {
            try
            {
                using (var conn = DatabaseManagerLite.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Documents WHERE id = @id";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", documentId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ خطأ في DeleteDocument: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// الحصول على وثيقة بواسطة المعرف
        /// </summary>
        public static DocumentModel GetDocumentById(int documentId)
        {
            try
            {
                using (var conn = DatabaseManagerLite.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Documents WHERE id = @id AND is_active = 1";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", documentId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new DocumentModel
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Title = reader["title"].ToString(),
                                    DocumentTypeId = Convert.ToInt32(reader["document_type_id"]),
                                    CategoryId = Convert.ToInt32(reader["category_id"]),
                                    FromDepartmentId = Convert.ToInt32(reader["from_department_id"]),
                                    ToDepartmentId = Convert.ToInt32(reader["to_department_id"]),
                                    BoxId = Convert.ToInt32(reader["box_id"]),
                                    DocumentDate = reader["document_date"]?.ToString(),
                                    ReceiveDate = reader["receive_date"]?.ToString(),
                                    IssueDate = reader["issue_date"]?.ToString(),
                                    UploadedBy = Convert.ToInt32(reader["uploaded_by"]),
                                    UploadedAt = reader["uploaded_at"]?.ToString(),
                                    UpdatedAt = reader["updated_at"]?.ToString(),
                                    Status = reader["status"]?.ToString(),
                                    Priority = reader["priority"]?.ToString(),
                                    DocumentNature = reader["document_nature"]?.ToString(),
                                    Summary = reader["summary"]?.ToString(),
                                    Notes = reader["notes"]?.ToString(),
                                    ArchiveDocNumber = reader["archiveDoc_number"]?.ToString(),
                                    ReferenceNumber = reader["ReferenceNumber"]?.ToString(),
                                    FilePath = reader["file_path"]?.ToString(),
                                    FileName = reader["file_name"]?.ToString(),
                                    FileType = reader["file_type"]?.ToString(),
                                    FileSize = Convert.ToInt64(reader["file_size"]),
                                    FileHash = reader["file_hash"]?.ToString(),
                                    IsActive = Convert.ToBoolean(reader["is_active"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ خطأ في GetDocumentById: {ex.Message}");
            }
            return null;
        }

        #endregion

        #region "دوال البحث"

        /// <summary>
        /// البحث في الوثائق
        /// </summary>
        public static DataTable SearchDocuments(string searchText, int boxId = 0)
        {
            try
            {
                string query = @"
                    SELECT 
                        id, title, document_date, status, priority,
                        file_name, file_size, archiveDoc_number,
                        (SELECT name FROM Document_Types WHERE id = Documents.document_type_id) as type_name,
                        (SELECT name FROM Document_Categories WHERE id = Documents.category_id) as category_name
                    FROM Documents 
                    WHERE is_active = 1";

                var parameters = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(searchText))
                {
                    query += " AND (title LIKE @search OR summary LIKE @search OR notes LIKE @search)";
                    parameters.Add("@search", $"%{searchText}%");
                }

                if (boxId > 0)
                {
                    query += " AND box_id = @boxId";
                    parameters.Add("@boxId", boxId);
                }

                query += " ORDER BY id DESC";

                return ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ خطأ في SearchDocuments: {ex.Message}");
                return new DataTable();
            }
        }

        #endregion

        #region "دوال التدقيق"

        /// <summary>
        /// تسجيل عملية في سجل التدقيق
        /// </summary>
        public static void LogAction(int userId, string action, string description, string ipAddress = null)
        {
            try
            {
                string sql = @"
                    INSERT INTO AuditTrail (user_id, action, description, ip_address, created_at)
                    VALUES (@userId, @action, @description, @ipAddress, CURRENT_TIMESTAMP)";

                var parameters = new Dictionary<string, object>
                {
                    { "@userId", userId },
                    { "@action", action },
                    { "@description", description ?? string.Empty },
                    { "@ipAddress", ipAddress ?? (object)DBNull.Value }
                };

                ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"⚠️ خطأ في تسجيل التدقيق: {ex.Message}");
            }
        }

        /// <summary>
        /// الحصول على سجل التدقيق
        /// </summary>
        public static DataTable GetAuditLog(int limit = 100)
        {
            try
            {
                string query = @"
                    SELECT id, user_id, action, description, ip_address, created_at
                    FROM AuditTrail
                    ORDER BY created_at DESC
                    LIMIT @limit";

                var parameters = new Dictionary<string, object> { { "@limit", limit } };
                return ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ خطأ في GetAuditLog: {ex.Message}");
                return new DataTable();
            }
        }

        #endregion
    }
}