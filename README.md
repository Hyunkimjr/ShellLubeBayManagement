# 🛢️ Shell LubeBay Management System

A desktop-based management system developed using *C# (Windows Forms)* and *MySQL* to streamline daily operations in a Shell Lube Bay.

---

## 📌 Overview

This system is designed to manage transactions, inventory, and customer records for an automotive service business. It replaces manual processes with a more efficient and organized digital solution.

---

## ⚙️ Features

- ✅ Transaction Management  
  - Record services, products, labor, and total cost  
  - Automatic total calculation  

- ✅ Customer & Product Integration  
  - Select customers and products from database  
  - Organized relational data handling  

- ✅ Inventory Management  
  - Track product quantities in real-time  
  - Manage lubricants, filters, and other items  

- ✅ Low Stock Warning System  
  - Monitors product quantities against reorder levels  
  - Alerts when stock reaches critical level  
  - Helps prevent out-of-stock situations  

- ✅ Monthly Sales Chart Analytics  
  - Visualizes monthly sales per product  
  - Tracks product performance trends  
  - Helps identify fast-moving and slow-moving items  

- ✅ Search Functionality  
  - Quickly find transactions and products  

- ✅ User-Friendly Interface  
  - Clean navigation (Customer, Product, Deliveries, Categories, Users, Transactions)
    
---

## 🖥️ System Screenshots

### Main Transaction Records
![Main Form](./screenshots/mainform.jpg)

### Transaction Module
![Transaction Module](./screenshots/transaction.jpg)

### Product Inventory Management
![Product Management](./screenshots/products.jpg)

### Sales Chart Analytics
![Sales Chart](./screenshots/chart.jpg)

---

## 🧠 Technical Highlights

- Designed and implemented a relational database using *MySQL*  
- Developed full CRUD functionality using *C# (Windows Forms)*  
- Implemented *DataGridView* for dynamic data display and management  
- Built modular system architecture (Customer, Product, Transaction modules)  

- Implemented *Low Stock Detection Logic*  
  - Compared product quantity against reorder threshold  
  - Enabled early identification of items needing restock  

- Developed *Monthly Sales Aggregation System*  
  - Processed transaction data by month  
  - Grouped and summarized product sales  

- Integrated *Data Visualization using Charts*  
  - Displayed monthly sales trends per product  
  - Improved decision-making through visual analytics  

- Applied real-world business logic based on actual Shell Lube Bay operations

---

## 🧠 Business Logic Implemented

- Inventory reorder level tracking  
- Monthly aggregation of sales data for charts  
- Data filtering by date (monthly reports)  
- Product-based sales performance tracking  
- Separation of concerns (Customer, Product, Transaction modules)

---

## 💡 Real-World Application

This system is based on actual operations from a Shell Lube Bay and can be used for:

- Automotive service centers  
- Inventory-based businesses  
- Small and medium enterprises  

---

## 🚀 Future Improvements

- User authentication (Admin/User roles)  
- Export reports to Excel  
- Receipt printing  
  
---

## 🛠️ Tools & Technologies

- C#
- Windows Forms
- MySQL
- Visual Studio 2022

---

## 👤 Author

*Hyun Jr. Kim*  
📧 hyunjrk@gmail.com  
📍 Santa Rosa, Laguna, Philippines
