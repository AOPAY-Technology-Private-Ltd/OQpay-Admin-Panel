<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TheEMIClubApplication.CommonPages.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">

        <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" rel="stylesheet" />

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <style>
    .btncolor:hover,
    .btn-success:hover {
        background-color: #CD7B2E !important;
        color: white !important;
    }
</style>
    <style>
    .bg-teal {
        background-color: #20c997 !important; /* Bootstrap teal */
    }
    .bg-indigo {
        background-color: #6610f2 !important; /* Bootstrap indigo */
    }
</style>
<style>
/* Main container wrapper */
.dashboard-wrapper {
  width: 100%;
  max-width: 1400px;
  margin: 0 auto;
  padding: 20px;
  box-sizing: border-box;
}

/* === GRID LAYOUTS === */
.dashboard-grid.grid-4 {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 20px;
}

.dashboard-grid.dual {
  display: grid;
  grid-template-columns: 1.4fr 2fr;
  gap: 20px;
}

@media (max-width: 1200px) {
  .dashboard-grid.grid-4 { grid-template-columns: repeat(3, 1fr); }
  .dashboard-grid.dual { grid-template-columns: 1fr; }
}

@media (max-width: 900px) {
  .dashboard-grid.grid-4 { grid-template-columns: repeat(2, 1fr); }
}

@media (max-width: 600px) {
  .dashboard-grid.grid-4 { grid-template-columns: 1fr; }
}

/* === CARD STYLES === */
.info-card {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-radius: 12px;
  padding: 20px 24px;
  color: #fff;
  box-shadow: 0 4px 12px rgba(0,0,0,0.08);
  transition: transform .2s ease, box-shadow .2s ease;
}
.info-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 22px rgba(0,0,0,0.12);
}

.info-card .details .label {
  font-size: 13px;
  opacity: .9;
  font-weight: 600;
}

.info-card .details .value {
  font-size: 20px;
  font-weight: 700;
}

.info-card .icon i {
  font-size: 38px;
  opacity: 0.4;
}

/* Larger featured card (compact version) */
.info-card.large-card {
  padding: 16px 20px; /* smaller padding */
  border-radius: 10px; /* slightly less rounded */
  box-shadow: 0 4px 12px rgba(0,0,0,0.1); /* subtle shadow */
}

/* Large card value styling */
.info-card.large-card .details .value {
  font-size: 22px; /* reduced from 28px for compactness */
  font-weight: 700;
  line-height: 1.2;
  margin-top: 4px;
}

/* Optional: small secondary value */
.info-card.large-card .details .small {
  font-size: 12px; /* slightly smaller */
  opacity: 0.85;
}


/* right grid small cards */
.right-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(180px, 1fr));
  gap: 15px;
}

/* === GRADIENTS === */
.gradient-orange { background: linear-gradient(135deg, #ff9966, #ff5e62); }
.gradient-green  { background: linear-gradient(135deg, #00c9a7, #92fe9d); }
.gradient-red    { background: linear-gradient(135deg, #ff6a6a, #ee0979); }
.gradient-blue   { background: linear-gradient(135deg, #00c6ff, #0072ff); }
.gradient-teal   { background: linear-gradient(135deg, #1de9b6, #1dc4e9); }
</style>

    <style>
/* Dashboard Grid: 5 equal cards */
.dashboard-grid.equal {
  display: flex;
  gap: 15px;
  flex-wrap: wrap;
  margin-top: 25px;
}

/* Each card same width */
.dashboard-grid.equal .info-card {
  flex: 1 1 calc(20% - 12px); /* 5 cards in a row */
  min-width: 150px; /* ensures responsiveness */
  padding: 16px 20px;
  border-radius: 10px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
  display: flex;
  justify-content: space-between;
  align-items: center;
  transition: transform 0.3s, box-shadow 0.3s;
}

.dashboard-grid.equal .info-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 20px rgba(0,0,0,0.15);
}

/* Card details */
.info-card .details .label {
  font-size: 14px;
  font-weight: 600;
  opacity: 0.85;
}

.info-card .details .value {
  font-size: 20px;
  font-weight: 700;
  margin-top: 4px;
}

.info-card .details .small {
  font-size: 12px;
  opacity: 0.85;
}

/* Icons */
.info-card .icon {
  font-size: 30px;
  opacity: 0.3;
}

/* Gradient Colors */
.gradient-orange { background: linear-gradient(135deg, #FFA726, #FB8C00); }
.gradient-blue { background: linear-gradient(135deg, #42A5F5, #1E88E5); }
.gradient-red { background: linear-gradient(135deg, #EF5350, #E53935); }
.gradient-green { background: linear-gradient(135deg, #66BB6A, #43A047); }
.gradient-purple { background: linear-gradient(135deg, #AB47BC, #8E24AA); }

/* Responsive for smaller screens */
@media(max-width: 1200px) {
  .dashboard-grid.equal .info-card { flex: 1 1 calc(50% - 12px); margin-bottom: 12px; }
}

@media(max-width: 768px) {
  .dashboard-grid.equal .info-card { flex: 1 1 100%; }
}
</style>


        <style>
        :root{
            --primary:#2563eb;
            --success:#16a34a;
            --warning:#f59e0b;
            --danger:#dc2626;
            --utility:#0ea5e9;
            --ecommerce:#9333ea;
            --dark:#111827;
            --gray:#6b7280;
            --light:#f8fafc;
            --gray-light:#e5e7eb;
        }


        /* ===== STAT ICON COLORS (SEMANTIC) ===== */
.icon-loan        { background:#f59e0b; } /* orange */
.icon-collected   { background:#16a34a; } /* green */
.icon-pending     { background:#dc2626; } /* red */
.icon-overdue     { background:#b91c1c; } /* dark red */
.icon-today       { background:#0ea5e9; } /* sky blue */
.icon-today-pend  { background:#2563eb; } /* blue */
.icon-retailer    { background:#15803d; } /* dark green */
.icon-customer    { background:#2563eb; } /* blue */
.icon-active-loan { background:#f59e0b; } /* orange */
.icon-membership  { background:#4f46e5; } /* indigo */
.icon-latefine    { background:#dc2626; } /* red */
.icon-processing  { background:#16a34a; } /* green */
.icon-interest    { background:#9333ea; } /* purple */
.icon-closed      { background:#166534; } /* dark green */
.icon-settlement  { background:#0284c7; } /* cyan */
.icon-foreclose   { background:#7f1d1d; } /* dark red */

        body{ background:#f4f6f9; }

        /* ===== LAYOUT ===== */
        .main-content{ padding:20px; }
        .topbar{
            display:flex;
            justify-content:space-between;
            align-items:center;
            background:#fff;
            padding:15px 20px;
            border-radius:12px;
            margin-bottom:20px;
            box-shadow:0 4px 12px rgba(0,0,0,.05);
        }

        .page-title h2{ margin:0; font-size:22px; }
        .page-title p{ margin:0; color:var(--gray); font-size:13px; }

        .user-menu{ display:flex; gap:20px; align-items:center; }
        .notifications{ position:relative; cursor:pointer; }
        .notification-badge{
            position:absolute;
            top:-6px; right:-6px;
            background:var(--danger);
            color:#fff;
            font-size:10px;
            padding:2px 6px;
            border-radius:50%;
        }

        .user-profile{
            display:flex; align-items:center; gap:10px; cursor:pointer;
        }
        .avatar{
            width:36px; height:36px;
            background:var(--primary);
            color:#fff;
            border-radius:50%;
            display:flex;
            align-items:center;
            justify-content:center;
            font-weight:600;
        }

      /* ===== STATS ===== */
.stats-container{
    display:grid;
    grid-template-columns:repeat(4, 1fr); /* ✅ 4 boxes per row */
    gap:20px;
    margin-bottom:20px;
}

/* Tablet */
@media (max-width: 992px){
    .stats-container{
        grid-template-columns:repeat(2, 1fr);
    }
}

/* Mobile */
@media (max-width: 576px){
    .stats-container{
        grid-template-columns:1fr;
    }
}

/* Card */
.stat-card{
    background:#fff;
    padding:18px;
    border-radius:14px;
    display:flex;
    gap:15px;
    align-items:center;
    box-shadow:0 8px 20px rgba(0,0,0,.06);
    transition:all .25s ease;
}

/* Hover effect (professional touch) */
.stat-card:hover{
    transform:translateY(-4px);
    box-shadow:0 12px 26px rgba(0,0,0,.12);
}

/* Icon */
.stat-icon{
    width:50px;
    height:50px;
    border-radius:12px;
    display:flex;
    align-items:center;
    justify-content:center;
    color:#fff;
    font-size:20px;
    flex-shrink:0;
}

/* Text */
.stat-info h3{
    margin:0;
    font-size:20px;
    font-weight:700;
}

.stat-info p{
    margin:0;
    font-size:13px;
    color:var(--gray);
}


        /* ===== TABLES ===== */
        .two-column{
            display:grid;
            grid-template-columns:2fr 1fr;
            gap:20px;
            margin-bottom:20px;
        }
        .table-container{
            background:#fff;
            border-radius:14px;
            box-shadow:0 8px 20px rgba(0,0,0,.06);
        }
        .table-header{
            display:flex;
            justify-content:space-between;
            align-items:center;
            padding:15px 20px;
            border-bottom:1px solid var(--gray-light);
        }
        table{
            width:100%;
            border-collapse:collapse;
            font-size:14px;
        }
        th,td{
            padding:12px 15px;
            border-bottom:1px solid var(--gray-light);
            text-align:left;
        }

        /* ===== STATUS ===== */
        .status{
            padding:4px 10px;
            border-radius:20px;
            font-size:12px;
            font-weight:600;
        }
        .status-success{ background:#dcfce7; color:var(--success); }
        .status-pending{ background:#fef3c7; color:var(--warning); }
        .status-failed{ background:#fee2e2; color:var(--danger); }
        .status-processing{ background:#e0e7ff; color:var(--primary); }

        /* ===== SERVICES ===== */
        .services-grid{
            display:grid;
            grid-template-columns:repeat(auto-fit,minmax(160px,1fr));
            gap:20px;
        }
        .service-card{
            background:var(--light);
            border-radius:14px;
            padding:20px;
            text-align:center;
            transition:.3s;
            cursor:pointer;
        }
        .service-card:hover{
            transform:translateY(-5px);
            box-shadow:0 12px 24px rgba(0,0,0,.1);
        }
        .service-icon{
            width:50px;height:50px;
            border-radius:50%;
            display:flex;
            align-items:center;
            justify-content:center;
            color:#fff;
            margin:0 auto 10px;
        }
        .mobile-bg{ background:#2563eb; }
        .dth-bg{ background:#f59e0b; }
        .biller-bg{ background:#16a34a; }
        .bus-bg{ background:#0ea5e9; }
        .air-bg{ background:#9333ea; }
        .electricity-bg{ background:#dc2626; }


        /* ===== LIVE + SERVICE MODULES ===== */
/*.module-grid{
    display:grid;
    grid-template-columns:repeat(auto-fit, minmax(100%, 1fr));
    gap:20px;
    margin-top:20px;
}*/

.module-grid{
    display:grid;
    grid-template-columns:2fr 1fr;
    gap:20px;
    margin-top:20px;
}

.module-card{
    background:#fff;
    border-radius:14px;
    box-shadow:0 8px 20px rgba(0,0,0,.06);
}

.module-header{
    display:flex;
    justify-content:space-between;
    align-items:center;
    padding:16px 20px;
    border-bottom:1px solid var(--gray-light);
}

.module-header h4{
    margin:0;
    font-size:16px;
    display:flex;
    align-items:center;
    gap:8px;
}

.btn-view{
    padding:6px 14px;
    border-radius:8px;
    border:1px solid var(--gray-light);
    background:#fff;
    font-size:13px;
    cursor:pointer;
}

.provider-icon{
    width:26px;
    text-align:center;
    font-size:16px;
}


/* ===== CHART GRID (2 COLUMNS) ===== */
/* ===== CHART GRID ===== */
.charts-grid {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 20px;
}

/* Chart container ensures canvas scales */
.chart-container {
    position: relative;
    width: 100%;
    height: 280px;
    padding: 10px;
}

/* Canvas must fill container */
.chart-container canvas {
    width: 100% !important;
    height: 100% !important;
}

/* ===== MOBILE FIX ===== */
@media (max-width: 992px) {
    .charts-grid {
        grid-template-columns: 1fr;
    }

    .chart-container {
        height: 240px;
    }
}

@media (max-width: 576px) {
    .chart-container {
        height: 220px;
    }

    .module-header h4 {
        font-size: 15px;
    }
}



/*.module-card canvas{
    min-height:260px;
}*/
.module-card canvas{
    width:100%!important;
}



/* ===== FILTER BAR ===== */
.filter-bar{
    background:#f9fafb;
    padding:16px;
    border-bottom:1px solid #e5e7eb;
}

/* ===== TABLE STYLING ===== */
.table th{
    font-size:13px;
    text-transform:uppercase;
    color:#6b7280;
    white-space:nowrap;
}

.table td{
    font-size:14px;
    white-space:nowrap;
}

.table-hover tbody tr:hover{
    background:#f8fafc;
}

/* ===== ACTION BUTTONS ===== */
.btn-outline-primary{
    border-radius:6px;
}

.toggle-switch{
    display:flex;
    background:#f1f5f9;
    border-radius:999px;
    padding:4px;
    box-shadow:inset 0 0 0 1px #e5e7eb;
}

.toggle-btn{
    padding:8px 18px;
    font-size:14px;
    font-weight:600;
    border-radius:999px;
    text-decoration:none;
    color:#374151;
    transition:all .25s ease;
}

/* HOVER EFFECT */
.toggle-btn:hover{
    background:#22c55e;
    color:#fff;
    box-shadow:0 4px 10px rgba(34,197,94,.35);
}

/* ACTIVE PAGE */
.toggle-btn.active{
    background:#16a34a;
    color:#fff;
    box-shadow:0 4px 10px rgba(22,163,74,.4);
}

.status-active{ background:#dcfce7; color:var(--success); }
.status-inactive{ background:#fee2e2; color:var(--danger); }
.title-icon{
    width:42px;
    height:42px;
    border-radius:10px;
    background:#2563eb;
    color:#fff;
    display:flex;
    align-items:center;
    justify-content:center;
    font-size:18px;
}


.btn-view.btn-outline{
    display:inline-flex;
    align-items:center;
    gap:6px;
    padding:7px 14px;
    font-size:13px;
    font-weight:600;
    border-radius:999px;
    border:1px solid #e5e7eb;
    color:#f59e0b;
    background:#fff;
    text-decoration:none;
    transition:all .25s ease;
}

.btn-view.btn-outline:hover{
    background:#f59e0b;
    color:#fff;
    border-color:#f59e0b;
    box-shadow:0 8px 18px rgba(37,99,235,.35);
}

.btn-view.btn-outline i{
    font-size:12px;
    transition:transform .25s ease;
}

.btn-view.btn-outline:hover i{
    transform:translateX(4px);
}


.clickable-card {
    cursor: pointer;
    transition: all 0.25s ease;
}

.clickable-card:hover {
    transform: translateY(-3px);
    box-shadow: 0 10px 25px rgba(0,0,0,0.15);
}


/* =====================================================
   FORCE RESPONSIVE CARD TABLE (FINAL FIX)
===================================================== */

//* ============================================
   FINAL RESPONSIVE TABLE FIX (ADMIN STANDARD)
============================================ */

/* Ensure module cards never overflow screen */
.module-card {
    max-width: 100%;
    overflow: hidden;
}

/* Table wrapper for horizontal scroll */
.module-card table {
    width: 100%;
    border-collapse: collapse;
}

/* Horizontal scroll on small & medium screens */
.table-responsive,
.module-card {
    overflow-x: auto;
    -webkit-overflow-scrolling: touch;
}

/* Keep rows single-line */
.module-card th,
.module-card td {
    white-space: nowrap;
}

/* Prevent table breaking layout on large screens */
@media (min-width: 6000) {
    .module-card {
        overflow-x: visible;
    }
}

/* Stack modules vertically on tablet & mobile */
@media (max-width: 992px) {
    .module-grid {
        grid-template-columns: 1fr;
    }
}


    </style>




    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>




    <!-- Bootstrap 5 CDN -->



<div class="main-content">
        <div  id="Div_Dealerdashboard" runat="server">

                  <span id="spnMessage" runat="server"></span>



<div class="topbar">

    <!-- Left : Page Title -->
<div class="page-title d-flex align-items-start gap-3">
    <%--<div class="title-icon">
  <i class="fas fa-tachometer-alt me-2 text-success"></i>
    </div>--%>
    <div>
        <h2>Dashboard</h2>
        <p>Manage your application efficiently</p>
    </div>
</div>


    <!-- Right : Toggle Switch -->
    <div class="user-menu">

<div class="toggle-switch">

    <a href="/Admin/ManageCustomer.aspx"
       class="toggle-btn"
       id="toggleCustomer">
        Customer
    </a>

    <a href="/Admin/ManageRetailer.aspx"
       class="toggle-btn"
       id="toggleDealer">
        Dealer
    </a>

</div>

    </div>

</div>


     





            


<div class="stats-container">

    <!-- Total Loan Released -->
    <div class="stat-card">
        <div class="stat-icon icon-loan">
            <i class="fas fa-hand-holding-usd"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblTotalLoanReleased" runat="server">0</span></h3>
            <p>Total Loan Released</p>
        </div>
    </div>

    <!-- Total Collected EMI -->
    <div class="stat-card">
        <div class="stat-icon icon-collected">
            <i class="fas fa-wallet"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblTotalCollectedEMI" runat="server">0</span></h3>
            <p>Total Collected EMI</p>
        </div>
    </div>

    <!-- Total Pending EMI -->
<div class="stat-card clickable-card"
     onclick="window.location.href='../Reports/LoanEmiStatusReport.aspx';"
     title="View Total Pending EMI Report">
        <div class="stat-icon icon-pending">
            <i class="fas fa-hourglass-half"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblTotalPendingEMI" runat="server">0</span></h3>
            <p>Total Pending EMI</p>
        </div>
    </div>

    <!-- Overdue Amount -->
    <div class="stat-card">
        <div class="stat-icon icon-overdue">
            <i class="fas fa-exclamation-circle"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblOverdueAmount" runat="server">0</span></h3>
            <p>Overdue Amount</p>
        </div>
    </div>

    <!-- Today Collected EMI -->
    <div class="stat-card">
        <div class="stat-icon icon-today">
            <i class="fas fa-calendar-day"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblTodayCollectedEMI" runat="server">0</span></h3>
            <p>Today Collected EMI</p>
        </div>
    </div>

    <!-- Today Pending EMI -->
<%--    <div class="stat-card">
        <div class="stat-icon icon-today-pend">
            <i class="fas fa-calendar-times"></i>
        </div>
        <div class="stat-info">
            <h3>₹<span id="lblTodayPendingEMI" runat="server">0</span></h3>
            <p>Today Pending EMI</p>
        </div>
    </div>--%>

     <!-- Total Active Loan -->
<div class="stat-card clickable-card"
     onclick="window.location.href='../Reports/LoanEmiStatusReport.aspx';"
     title="View Active Loan Report">

    <div class="stat-icon icon-active-loan">
        <i class="fas fa-briefcase"></i>
    </div>

    <div class="stat-info">
        <h3>
            <span id="lblTotalloan" runat="server">0</span>
        </h3>
        <p>Total Active Loan</p>
    </div>
</div>



        <!-- Total Closed Loan -->
    <div class="stat-card">
        <div class="stat-icon icon-closed">
            <i class="fas fa-lock"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblTotalClosedLoan" runat="server">0</span></h3>
            <p>Total Closed Loan</p>
        </div>
    </div>

    <!-- Total Settlement -->
<div class="stat-card clickable-card"
     onclick="window.location.href='../Reports/LoanSettlementForeclosureReport.aspx';"
     title="View Active Loan Report">
        <div class="stat-icon icon-settlement">
            <i class="fas fa-handshake"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblTotalSettlement" runat="server">0</span></h3>
            <p>Total Settlement Loan</p>
        </div>
    </div>

    <!-- Total Foreclose -->
<div class="stat-card clickable-card"
     onclick="window.location.href='../Reports/LoanSettlementForeclosureReport.aspx';"
     title="View Active Loan Report">
        <div class="stat-icon icon-foreclose">
            <i class="fas fa-ban"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblTotalForeclose" runat="server">0</span></h3>
            <p>Total Foreclose Loan</p>
        </div>
    </div>


  

   
    <!-- Membership Charge -->
   <div class="stat-card clickable-card"
     onclick="window.location.href='../Reports/AllReports.aspx';"
     title="View Membership Report">
        <div class="stat-icon icon-membership">
            <i class="fas fa-id-card"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblMembershipCharge" runat="server">0</span></h3>
            <p>Membership Charge</p>
        </div>
    </div>

    <!-- Late Fine -->
   <div class="stat-card clickable-card"
     onclick="window.location.href='../Reports/AllReports.aspx';"
     title="View Late Fine Report">
        <div class="stat-icon icon-latefine">
            <i class="fas fa-exclamation-circle"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblLateFine" runat="server">0</span></h3>
            <p>Late Fine</p>
        </div>
    </div>

    <!-- Processing Fee -->
    <div class="stat-card clickable-card"
     onclick="window.location.href='../Reports/AllReports.aspx';"
     title="View Processing Fee Report">
        <div class="stat-icon icon-processing">
            <i class="fas fa-cogs"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblProcessingFee" runat="server">0</span></h3>
            <p>Processing Fee</p>
        </div>
    </div>

    <!-- Total Interest -->
    <div class="stat-card clickable-card"
     onclick="window.location.href='../Reports/AllReports.aspx';"
     title="View Total Interest Report">
        <div class="stat-icon icon-interest">
            <i class="fas fa-percent"></i>
        </div>
        <div class="stat-info">
            <h3><span id="lblTotalInterest" runat="server">0</span></h3>
            <p>Total Interest</p>
        </div>
    </div>


      <!-- Total Retailers -->
   <div class="stat-card clickable-card"
    onclick="window.location.href='../admin/ManageRetailer.aspx';"
    title="View Retailers">
      <div class="stat-icon icon-retailer">
          <i class="fas fa-store"></i>
      </div>
      <div class="stat-info">
          <h3><span id="lblTotalRetailers" runat="server">0</span></h3>
          <p>Total Retailers</p>
      </div>
  </div>

  <!-- Total Customers -->
  <div class="stat-card clickable-card"
 onclick="window.location.href='../admin/ManageCustomer.aspx';"
 title="View Customers">
      <div class="stat-icon icon-customer">
          <i class="fas fa-users"></i>
      </div>
      <div class="stat-info">
          <h3><span id="lblTotalCustomers" runat="server">0</span></h3>
          <p>Total Customers</p>
      </div>
  </div>

</div>









    <!-- Overdue EMI -->


    <!-- Customer Loan Summary -->
<div class="module-grid">

    <!-- ===== CUSTOMER LOAN SUMMARY ===== -->
    <div class="module-card">

        <!-- Header -->
        <div class="module-header">
            <h4>
                <i class="fas fa-file-invoice-dollar text-primary"></i>
                Customer Loan Summary
            </h4>
       <a href="../admin/Customerloandetails.aspx" class="btn-view btn-outline" style="background:var(--primary);color:#fff;">
    View More
    <i class="fas fa-arrow-right"></i>
</a>

        </div>

        <!-- Table -->
        <div class="table-responsive">
            <table>
                <thead>
                    <tr>
                        <th>Customer Code</th>
                        <th>Loan Code</th>
                        <th>Loan Amount</th>
                        <th>Total EMI</th>
                        <th>Collected EMI</th>
                        <th>Pending EMI</th>
                        <th>Status</th>
                        <th>Brand</th>
                        <th>Model</th>
                        <th>Variant</th>
                    </tr>
                </thead>
                <tbody id="tblCustomerLoan">
                    <!-- Dynamic rows inserted via JS -->
                </tbody>
            </table>
        </div>



    </div>
            <div class="module-card">
        <div class="module-header">
 <h4><i class="fas fa-crown text-warning"></i> Top Retailer-Active Loan</h4>
       
        </div>

        <table>
            <thead>
                <tr>
                   <th>Retailer</th>
<th>Loans</th>
<th>Loan ₹</th>
<th>EMI Exp.</th>
<th>EMI Coll.</th>
<th>EMI Pend.</th>
<th>EMI Ovd.</th>
<th>Eff. %</th>
                </tr>
            </thead>
  <tbody id="tblTopretailer">
      <!-- Dynamic rows inserted via JS -->
  </tbody>
        </table>
    </div> 

</div>


  





       


<div class="module-grid charts-grid">

    <div class="module-card">
        <div class="module-header">
            <h4>
                <i class="fas fa-chart-line text-success"></i>
                Month-wise Collected EMI
            </h4>
        </div>
        <div class="chart-container">
            <canvas id="chartCollectedEMI"></canvas>
        </div>
    </div>

    <div class="module-card">
        <div class="module-header">
            <h4>
                <i class="fas fa-chart-bar text-warning"></i>
                Month-wise Pending EMI
            </h4>
        </div>
        <div class="chart-container">
            <canvas id="chartPendingEMI"></canvas>
        </div>
    </div>

</div>

            



<div id="divemidetails" runat="server" visible="false">

    <div class="module-card">

        <!-- HEADER -->
        <div class="module-header">
            <h4>
                <i class="fas fa-file-invoice-dollar text-primary"></i>
                Loan & EMI Details
            </h4>
        </div>

        <!-- FILTER BAR -->
        <div class="filter-bar">
            <div class="row g-3 align-items-end">

                <div class="col-md-3">
                    <label class="form-label fw-semibold">Criteria</label>
                    <asp:DropDownList ID="ddlEmiCriteria" runat="server" CssClass="form-select">
                        <asp:ListItem Text="All" Value="0" />
                        <asp:ListItem Text="Loan Id" Value="loanid" />
                        <asp:ListItem Text="User Id" Value="userid" />
                        <asp:ListItem Text="Active" Value="Active" />
                        <asp:ListItem Text="Inactive" Value="Inactive" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label class="form-label fw-semibold">Value</label>
                    <asp:TextBox ID="txtEmivalue" runat="server"
                        CssClass="form-control"
                        Placeholder="Enter value" />
                </div>

                <div class="col-md-3">
                    <asp:Button ID="btnEMISearch" runat="server"
                        Text="Search"
                        CssClass="btn btn-primary w-100"
                        OnClick="btnEMISearch_Click" />
                </div>

            </div>
        </div>

        <!-- TABLE -->
        <div class="table-responsive p-3">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>

                    <asp:GridView ID="grdEmaiDetails" runat="server"
                        AutoGenerateColumns="false"
                        CssClass="table table-hover align-middle mb-0"
                        PageSize="5"
                        AllowPaging="true"
                        DataKeyNames="LoanRID"
                        OnRowCommand="grdEmaiDetails_RowCommand"
                        OnPageIndexChanging="grdEmaiDetails_PageIndexChanging"
                        GridLines="None">

                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="#" />
                            <asp:BoundField DataField="LoanCode" HeaderText="Loan ID" />
                            <asp:BoundField DataField="CustomerCode" HeaderText="User Code" />
                            <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amt" />
                            <asp:BoundField DataField="DownPayment" HeaderText="DP" />
                            <asp:BoundField DataField="LoanEMIAmount" HeaderText="EMI Amt" />
                            <asp:BoundField DataField="Tenure" HeaderText="Tenure" />
                            <asp:BoundField DataField="InterestRate" HeaderText="Inst (%)" />
                            <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                            <asp:BoundField DataField="EndDate" HeaderText="End Date" />
                            <asp:BoundField DataField="LoanStatus" HeaderText="Status" />

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server"
                                        Text="View"
                                        CssClass="btn btn-sm btn-outline-primary"
                                        CommandName="EditRow"
                                        CommandArgument='<%# Eval("LoanRID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                    <asp:Label ID="lblEMINoData" runat="server"
                        CssClass="mt-2 fw-semibold text-danger d-block" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>
</div>
        
    
<!-- ===== EMI DETAILS SECTION ===== -->
<div class="module-grid">

    <div class="module-card" runat="server" id="currentemi">

        <!-- HEADER -->
        <div class="module-header">
            <h4>
                <i class="fas fa-clock text-warning"></i>
                EMI Details
            </h4>
        </div>

        <!-- FILTER BAR -->
        <div class="filter-bar">
            <div class="row g-3 align-items-end">

                <div class="col-md-3">
                    <label class="form-label fw-semibold">Case Type</label>
                    <asp:DropDownList ID="ddlcasetype" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Over Due" Value="OVERDUE" />
                        <asp:ListItem Text="Today Due" Value="CURRENT" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label class="form-label fw-semibold">Filter By</label>
                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All" Value="0" />
                        <asp:ListItem Text="Loan Id" Value="loanid" />
                        <asp:ListItem Text="User Id" Value="userid" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label class="form-label fw-semibold">Value</label>
                    <asp:TextBox ID="TextBox2" runat="server"
                        CssClass="form-control"
                        Placeholder="Enter value" />
                </div>

                <div class="col-md-3">
                    <asp:Button ID="Button4" runat="server"
                        Text="Search"
                        CssClass="btn btn-primary w-100"
                        OnClick="Button4_Click" />
                </div>

            </div>
        </div>

        <!-- TABLE -->
        <div class="table-responsive p-3">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>

                    <asp:GridView ID="GridView_CurrentEMI" runat="server"
                        AutoGenerateColumns="false"
                        CssClass="table table-hover align-middle mb-0"
                        PageSize="10"
                        AllowPaging="true"
                        DataKeyNames="RID"
                        OnPageIndexChanging="GridView_CurrentEMI_PageIndexChanging"
                        OnRowCommand="GridView_CurrentEMI_RowCommand"
                        GridLines="None">

                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="#" />
                            <asp:BoundField DataField="LoanCode" HeaderText="Loan ID" />
                            <asp:BoundField DataField="CustomerCode" HeaderText="User Code" />
                            <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amt" />
                            <asp:BoundField DataField="DownPayment" HeaderText="DP" />
                            <asp:BoundField DataField="EMIAmount" HeaderText="EMI Amt" />
                            <asp:BoundField DataField="Tenure" HeaderText="Tenure" />
                            <asp:BoundField DataField="InterestRate" HeaderText="Inst (%)" />
                            <asp:BoundField DataField="PaidEMI" HeaderText="Paid EMI" />
                            <asp:BoundField DataField="DuesEMI" HeaderText="Due EMI" />
                            <asp:BoundField DataField="NextDueDate"
                                HeaderText="Next Due Date"
                                DataFormatString="{0:dd-MM-yyyy}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="RecordStatus" HeaderText="Status" />

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server"
                                        Text="View"
                                        CssClass="btn btn-sm btn-outline-primary"
                                        CommandName="EditRow"
                                        CommandArgument='<%# Eval("RID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>

</div>
              </div>

    
       

        <div class="card shadow-sm" id="Div_Employeedashboard" runat="server" >
                        <div class="d-flex flex-column flex-sm-row justify-content-between align-items-center text-center text-sm-start mb-4" style="background-color: #006AAE; border-top-right-radius: 8px; border-top-left-radius: 8px;">
                <!-- Heading -->
                <div style="color: white;">
                    <h2 style="color: white;">&nbsp;&nbsp;Dashboard</h2>
                </div>
                <!-- Navigation Links -->
<%--              <nav class="d-flex flex-column flex-sm-row">
    <asp:Button ID="Button1" runat="server"
        CssClass="btn btn btn- me-sm-2 mb-2 btncolor mb-sm-0"
        Text="Customer" Visible="false"
        OnClick="btnvisibalCustomer_Click"
        style="background-color:#50C878; color:white;" />
        
    <asp:Button ID="Button2" runat="server"
        CssClass="btn btn btn-success me-sm-2 mb-2 mb-sm-0 text-white"
        Text="Dealer" Visible="false"
        OnClick="btnbvisibalDealer_Click"
        style="background-color:#50C878; color:white;" />
</nav>--%>

            </div>

<div class="container-fluid mt-4">

    <!-- Cards Row -->
<div class="row">

        <!-- Total Collected EMI -->
        <div class="col-lg-3 col-md-6 mb-4">
            <div class="card shadow h-100 text-white bg-success border-0">
                <div class="card-body d-flex align-items-center justify-content-between">
                    <div>
                        <h6 class="card-title font-weight-bold">Total Collected EMI</h6>
                        <h3 class="card-text">
₹ <span id="lblEmpTotalCollectedEMI" runat="server"> 0</span>
                        </h3>
                    </div>
                    <div class="card-icon display-4 opacity-25">
                        <i class="fas fa-hand-holding-usd"></i>
                    </div>
                </div>
            </div>
        </div>

        <!-- Total Pending EMI -->
        <div class="col-lg-3 col-md-6 mb-4">
            <div class="card shadow h-100 text-white bg-warning border-0">
                <div class="card-body d-flex align-items-center justify-content-between">
                    <div>
                        <h6 class="card-title font-weight-bold">Total Pending EMI</h6>
                                                <h3 class="card-text">
₹ <span id="lblEmpTotalPendingEMI" runat="server">
                             0</span>
                        </h3>
                    </div>
                    <div class="card-icon display-4 opacity-25">
                        <i class="fas fa-clock"></i>
                    </div>
                </div>
            </div>
        </div>

        <!-- Total Overdue EMI -->
        <div class="col-lg-3 col-md-6 mb-4">
            <div class="card shadow h-100 text-white bg-danger border-0">
                <div class="card-body d-flex align-items-center justify-content-between">
                    <div>
                        <h6 class="card-title font-weight-bold">Total Overdue EMI</h6>
                                                <h3 class="card-text">
₹ <span id="lblEmpTotalOverdueEMI" runat="server">
                             0</span>
                        </h3>
                    </div>
                    <div class="card-icon display-4 opacity-25">
                        <i class="fas fa-exclamation-triangle"></i>
                    </div>
                </div>
            </div>
        </div>

        <!-- Total Loans Assigned -->
<div class="col-lg-3 col-md-6 mb-4">
    <a href="/Admin/ShowAssignEmiDetailstoEmployee.aspx"
       style="text-decoration:none;">

        <div class="card shadow h-100 text-white bg-info border-0 cursor-pointer">
            <div class="card-body d-flex align-items-center justify-content-between">
                <div>
                    <h6 class="card-title font-weight-bold">Total Loans Assigned</h6>
                    <h3 id="lblEmpTotalLoansAssigned" runat="server">
                        <i class="fas fa-rupee-sign"></i> 0
                    </h3>
                </div>
                <div class="card-icon display-4 opacity-25">
                    <i class="fas fa-file-alt"></i>
                </div>
            </div>
        </div>

    </a>
</div>


    </div>

    <!-- Charts Row -->
    <div class="row mt-4">
        <!-- Month-wise Collected EMI Chart -->
   <div class="col-md-6 mb-4">
       <div class="card shadow-sm">
           <div class="card-header bg-primary text-white">
               <i class="fas fa-chart-line"></i> Month-wise Collected EMI
           </div>
           <div class="card-body">
                    <canvas id="chartEmpCollectedEMI" height="200"></canvas>
                </div>
            </div>
        </div>

        <!-- Month-wise Pending EMI Chart -->
   <div class="col-md-6 mb-4">
       <div class="card shadow-sm">
           <div class="card-header bg-primary text-white">
               <i class="fas fa-chart-line"></i> Month-wise Pending EMI
           </div>
           <div class="card-body">
                    <canvas id="chartEmpPendingEMI" height="200"></canvas>
                </div>
            </div>
        </div>
    </div>

</div>


        </div>

            <div id="div1" class="card card-default  p-2 m-2" runat="server" visible="false" >
        <%-- Customer EMI Details--%>
        <div class="card-header form-header-bar" id="DivAssignEMIDetails" style="background-color: transparent;">
            <h3 class="card-title mb-0" style="color: black; background-color: transparent;">Assigned EMI</h3>
        </div>
        <div class="card-body">
            <div class="col-md-12 card-body-box m-2">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <span class="font-weight-bold">Criteria</span>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                <asp:ListItem id="emp_EMIAll" Text="All" Value="0"></asp:ListItem>
                                <%-- <asp:ListItem id="Emiid" Text="EMI Id" Value="Emiid"></asp:ListItem>--%>
                                <asp:ListItem id="emp_loanid" Text="Loan Id" Value="loanid"></asp:ListItem>
                                <asp:ListItem id="emp_userid" Text="User Id" Value="userid"></asp:ListItem>
                                <asp:ListItem id="emp_EMIActive" Text="Active" Value="Active"></asp:ListItem>
                                <asp:ListItem id="emp_EMIInactive" Text="Inactive" Value="Inactive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <span class="font-weight-bold">Value</span>
                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" Placeholder="Enter Value"></asp:TextBox>


                        </div>
                    </div>
                    <div class="col-md-6  d-flex align-items-center">
                        <span class="font-weight-bold"></span>
                        <asp:Button ID="Button3" runat="server" Text="Search" class="btn btn-primary"  />
                    </div>
                </div>
            </div>
            <div class="container-fluid my-3">
                <div class="table-responsive">
                    <span id="Span3" runat="server"></span>

                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdAssignView" runat="server"
                                AutoGenerateColumns="false"
                                PageSize="5"
                                CssClass="table table-bordered table-striped table-hover w-100 mb-0"
                                AllowPaging="true"
                                OnRowCommand="grdAssignView_RowCommand"
                                DataKeyNames="RID"
                                PagerSettings-Mode="Numeric"
                                PagerSettings-Position="Bottom"
                                PagerStyle-HorizontalAlign="Center"
                                PagerStyle-CssClass="pagination-container"
                                UseAccessibleHeader="true"
                                GridLines="None">

                                        

                                <Columns>
                                    <%-- Optional: Use code-behind for SrNo calculation --%>
                                                  <asp:BoundField DataField="SrNo" HeaderText="S.No" />
      <asp:BoundField DataField="LoanCode" HeaderText="Loan ID" />
      <asp:BoundField DataField="CustomerCode" HeaderText="User Code" />
      <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amt." />
      <asp:BoundField DataField="DownPayment" HeaderText="DP" />
      <asp:BoundField DataField="EMIAmount" HeaderText="EMI Amt." />
      <asp:BoundField DataField="Tenure" HeaderText="Tenure" />
      <asp:BoundField DataField="InterestRate" HeaderText="Inst.(%)" />
      <asp:BoundField DataField="PaidEMI" HeaderText="PaidEMI" />
      <asp:BoundField DataField="DuesEMI" HeaderText="DuesEMI" />
      <asp:BoundField DataField="FirstDueDate" HeaderText="FirstDueDate" />
      <asp:BoundField DataField="RecordStatus" HeaderText="RecordStatus"  />
      <asp:BoundField DataField="flag_Status" HeaderText="Assigned"  />
                                            <asp:BoundField DataField="RID" HeaderText="" Visible="false" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server"
                                                Text="View"
                                                CssClass="btn btn-sm btn-primary"
                                                CommandName="EditRow"
                                                CommandArgument='<%# Eval("RID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
            <ItemTemplate>
               
                <asp:HiddenField ID="hfEMIAmt" runat="server" Value='<%# Eval("EMIAmount") %>'  />

                <asp:HiddenField ID="hfPaidEMI" runat="server" Value='<%# Eval("PaidEMI") %>' />
                ...
            </ItemTemplate>
        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="FollowUp">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkFollowup" runat="server"
                                                Text="Followup"
                                                CssClass="btn btn-sm btn-primary"
                                                CommandName="Followup"
                                                CommandArgument='<%# Eval("LoanCode") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>

                            <asp:Label ID="Label2" runat="server"
                                CssClass="mt-2 font-weight-bold d-block">
                            </asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>


        </div>



    </div>

        <!-- Cards Section -->

  
    
   </div>
                    <div id="ErrorPage" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Error Message
                    </h5>
                </div>
                <div class="modal-body">
                    <span id="lblerror" style="font-family: Georgia, 'Times New Roman', Times, serif; font-size: medium; color: red; font-weight: bold"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal" id="">
                        Close</button>
                </div>
            </div>
        </div>
    </div>

        <div id="MyPopups" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header" style="background-color:cornflowerblue">
              
                <h5 class="modal-title"> Confirmation Messages
                </h5>
            </div>
            <div class="modal-body">
                
           
                  <span id="lblMessages" style="font-family:Georgia, 'Times New Roman', Times, serif;font-size:medium;color:forestgreen" ></span>
                  <br />
                <span id="lblName" style="color:navy"></span>
              
            </div>
            <div class="modal-footer">
                <button type="button"  data-dismiss="modal" class="btn-success" id="btnsucess" runat="server" causesvalidation="false" >
                    Ok</button>
            </div>
        </div>
    </div>
</div>


           <script type="text/javascript">
               function ShowPopup(userid, messages) {
                   $("#MyPopups #lblName").html(userid);
                   $("#MyPopups #lblMessages").html(messages);
                   $("#MyPopups").modal("show");
               }
           </script>

   <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

<script>
    $(document).ready(function () {

        // ============================
        // Utility: Animate Counters
        // ============================
        function animateValue(id, endValue, prefix = '', suffix = '', duration = 1000) {
            const el = $(id);
            endValue = Number(endValue) || 0;

            if (endValue === 0) {
                el.text(prefix + '0' + suffix);
                return;
            }

            let startValue = 0;
            const increment = Math.ceil(endValue / 50);
            const stepTime = Math.max(Math.floor(duration / 50), 20);

            const timer = setInterval(() => {
                startValue += increment;
                if (startValue >= endValue) {
                    startValue = endValue;
                    clearInterval(timer);
                }
                el.text(prefix + startValue.toLocaleString() + suffix);
            }, stepTime);
        }

        // ============================
        // AJAX: Load Dashboard Metrics
        // ============================
        $.ajax({
            type: "POST",
            url: "Home.aspx/GetMetrics",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                const data = response.d || {};

                // ============================
                // Top Summary Cards
                // ============================
                animateValue('#<%= lblTotalRetailers.ClientID %>', data.TotalRetailer);
                animateValue('#<%= lblTotalCustomers.ClientID %>', data.Totalcustomer);

                animateValue('#<%= lblTodayCollectedEMI.ClientID %>', data.TotaltodayCollectedEMI, '₹ ');
           <%--     animateValue('#<%= lblTodayPendingEMI.ClientID %>', data.TotaltotalPendingEMI, '₹ ');--%>

                animateValue('#<%= lblProcessingFee.ClientID %>', data.TotalProcessfee, '₹ ');
                animateValue('#<%= lblTotalInterest.ClientID %>', data.Totalinterrestamt, '₹ ');
                animateValue('#<%= lblMembershipCharge.ClientID %>', data.Totalmembershipfee, '₹ ');
                animateValue('#<%= lblLateFine.ClientID %>', data.TotalLatefine, '₹ ');

                // ============================
                // Loan Status Cards (NEW)
                // ============================
                animateValue('#<%= lblTotalloan.ClientID %>', data.TotalActiveLoans);
                animateValue('#<%= lblTotalClosedLoan.ClientID %>', data.TotalClosedLoans);
                animateValue('#<%= lblTotalForeclose.ClientID %>', data.TotalForeclosureLoans);
                animateValue('#<%= lblTotalSettlement.ClientID %>', data.TotalSettlementLoans);

                // ============================
                // Top Retailers
                // ============================
                //const $ulTopRetailers = $('#ulTopRetailers').empty();
                //(data.TopRetailers || []).slice(0, 10).forEach(r => {
                //    $ulTopRetailers.append(
                //        `<li><strong>${r.RetailerCode}</strong> — ₹ ${Number(r.TotalCollectedEMI || 0).toLocaleString()}</li>`
                //    );
                //});

                // ============================
                // Overdue EMI List
                // ============================
                const $ulOverdueEMI = $('#ulOverdueEMI').empty();
                (data.OverdueEMI || []).slice(0, 10).forEach(o => {
                    $ulOverdueEMI.append(
                        `<li><strong>${o.CustomerCode}</strong> — ₹ ${Number(o.OverdueAmount || 0).toLocaleString()}</li>`
                    );
                });

                // ============================
                // Destroy Existing Charts
                // ============================
                if (window.collectedChart) window.collectedChart.destroy();
                if (window.pendingChart) window.pendingChart.destroy();

                // ============================
                // Collected EMI Chart
                // ============================
                const collectedCanvas = document.getElementById('chartCollectedEMI');
                const ctxCollected = collectedCanvas.getContext('2d');

                // Destroy old chart if exists
                if (window.collectedChart) {
                    window.collectedChart.destroy();
                }

                // Create gradient dynamically
                const gradientCollected = ctxCollected.createLinearGradient(0, 0, 0, collectedCanvas.parentElement.offsetHeight);
                gradientCollected.addColorStop(0, 'rgba(16, 185, 129, 0.35)');
                gradientCollected.addColorStop(1, 'rgba(16, 185, 129, 0.05)');

                window.collectedChart = new Chart(ctxCollected, {
                    type: 'line',
                    data: {
                        labels: (data.MonthWiseCollectedEMI || []).map(x => `${x.Month}/${x.Year}`),
                        datasets: [{
                            label: 'Collected EMI',
                            data: (data.MonthWiseCollectedEMI || []).map(x => x.CollectedEMI),
                            fill: true,
                            backgroundColor: gradientCollected,
                            borderColor: '#10b981',
                            borderWidth: 3,
                            tension: 0.4,
                            pointRadius: 4,
                            pointHoverRadius: 7,
                            pointBackgroundColor: '#10b981'
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        animation: {
                            duration: 900,
                            easing: 'easeOutQuart'
                        },
                        plugins: {
                            legend: {
                                display: true,
                                position: 'top'
                            },
                            tooltip: {
                                callbacks: {
                                    label: ctx => `₹ ${ctx.raw.toLocaleString()}`
                                }
                            }
                        },
                        scales: {
                            x: {
                                grid: { display: false }
                            },
                            y: {
                                ticks: {
                                    callback: value => '₹ ' + value.toLocaleString()
                                },
                                grid: {
                                    color: '#e5e7eb'
                                }
                            }
                        }
                    }
                });


                // ============================
                // Pending EMI Chart
                // ============================
                // ============================
                // Pending EMI - Modern Area Chart
                // ============================
                const canvas = document.getElementById('chartPendingEMI');
                const ctx = canvas.getContext('2d');

                // Destroy previous chart
                if (window.pendingChart) {
                    window.pendingChart.destroy();
                }

                // ✅ SAFE DATA HANDLING
                const source = data.MonthWisePendingEMI || [];

                const labels = source.map(x => `${x.Month}/${x.Year}`);
                const values = source.map(x => Number(x.PendingEMI || 0));

                // Gradient
                const barGradient = ctx.createLinearGradient(0, 0, 0, 300);
                barGradient.addColorStop(0, 'rgba(245, 158, 11, 0.9)');
                barGradient.addColorStop(1, 'rgba(245, 158, 11, 0.3)');

                window.pendingChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Pending EMI',
                            data: values,
                            backgroundColor: barGradient,
                            borderRadius: 12,
                            borderSkipped: false,
                            barThickness: 28,
                            hoverBackgroundColor: '#f59e0b'
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        animation: {
                            duration: 1200,
                            easing: 'easeOutQuart'
                        },
                        plugins: {
                            legend: { display: false },
                            tooltip: {
                                backgroundColor: '#111827',
                                titleColor: '#fff',
                                bodyColor: '#fbbf24',
                                padding: 12,
                                callbacks: {
                                    label: ctx => ` ₹ ${ctx.raw.toLocaleString()}`
                                }
                            }
                        },
                        scales: {
                            x: {
                                grid: { display: false },
                                ticks: { color: '#6b7280' }
                            },
                            y: {
                                grid: { color: '#fde68a' },
                                ticks: {
                                    color: '#6b7280',
                                    callback: v => `₹ ${v.toLocaleString()}`
                                }
                            }
                        }
                    }
                });




                // ============================
                // Customer Loan Summary Table
                // ============================
                const $tbody = $('#tblCustomerLoan').empty();

                (data.CustomerLoanSummary || []).slice(0, 5).forEach(row => {

                    let statusClass = 'status-processing';
                    const status = (row.LoanStatus || '').toLowerCase();

                    if (['active', 'completed', 'success'].includes(status)) statusClass = 'status-success';
                    else if (status === 'pending') statusClass = 'status-pending';
                    else if (['failed', 'overdue'].includes(status)) statusClass = 'status-failed';

                    $tbody.append(`
        <tr>
            <td data-label="Customer">${row.CustomerCode ?? '-'}</td>
            <td data-label="Loan">${row.LoanCode ?? '-'}</td>
            <td data-label="Loan ₹">${Number(row.LoanAmount || 0).toLocaleString()}</td>
            <td data-label="Total EMI">${Number(row.TotalEMI || 0).toLocaleString()}</td>
            <td data-label="EMI Coll.">${Number(row.CollectedEMI || 0).toLocaleString()}</td>
            <td data-label="EMI Pend.">${Number(row.PendingEMI || 0).toLocaleString()}</td>
            <td data-label="Status">
                <span class="status ${statusClass}">
                    ${row.LoanStatus ?? 'Processing'}
                </span>
            </td>
            <td data-label="Brand">${row.BrandName ?? '-'}</td>
            <td data-label="Model">${row.ModelName ?? '-'}</td>
            <td data-label="Variant">${row.VariantName ?? '-'}</td>
        </tr>
    `);
                });




                const $retailerTbody = $('#tblTopretailer').empty();

                (data.TopRetailers || []).slice(0, 5).forEach(row => {

                    // Collection efficiency status
                    let efficiencyClass = 'status-processing';
                    const efficiency = Number(row.CollectionEfficiency || 0);

                    if (efficiency >= 90) efficiencyClass = 'status-success';
                    else if (efficiency >= 70) efficiencyClass = 'status-pending';
                    else efficiencyClass = 'status-failed';

                    $retailerTbody.append(`
        <tr>
            <td data-label="Retailer Code">${row.RetailerCode ?? '-'}</td>

            <td data-label="Active Loans">
                ${Number(row.TotalActiveLoans || 0).toLocaleString()}
            </td>

            <td data-label="Loan Amount">
                 ${Number(row.TotalLoanAmount || 0).toLocaleString()}
            </td>

            <td data-label="Expected EMI">
                 ${Number(row.TotalEMIExpected || 0).toLocaleString()}
            </td>

            <td data-label="Collected EMI">
                 ${Number(row.TotalCollectedEMI || 0).toLocaleString()}
            </td>

            <td data-label="Pending EMI">
                 ${Number(row.PendingEMIAmount || 0).toLocaleString()}
            </td>

            <td data-label="Overdue EMI">
                 ${Number(row.OverdueEMIAmount || 0).toLocaleString()}
            </td>

            <td data-label="Efficiency">
                <span class="status ${efficiencyClass}">
                    ${efficiency.toFixed(1)}%
                </span>
            </td>
        </tr>
    `);
                });

            },
            error: function (xhr, status, error) {
                console.error("Dashboard load error:", error);
            }
        });
    });
</script>


    <script>
        function animateValue(selector, end, isCurrency = true, duration = 1500) {
            var obj = $(selector);
            var start = 0;
            end = Number(end) || 0;
            var range = end - start;
            var startTime = performance.now();

            function step(currentTime) {
                var progress = Math.min((currentTime - startTime) / duration, 1);
                var value = Math.floor(progress * range + start);
                if (isCurrency) {
                    obj.text('₹ ' + value.toLocaleString());
                } else {
                    obj.text(value.toLocaleString());
                }
                if (progress < 1) requestAnimationFrame(step);
            }
            requestAnimationFrame(step);
        }

        $(document).ready(function () {
            // Run animation for loan summary labels
            animateValue('#<%= lblTotalLoanReleased.ClientID %>', $('#<%= lblTotalLoanReleased.ClientID %>').data('value'), true);
    animateValue('#<%= lblTotalCollectedEMI.ClientID %>', $('#<%= lblTotalCollectedEMI.ClientID %>').data('value'), true);
    animateValue('#<%= lblTotalPendingEMI.ClientID %>', $('#<%= lblTotalPendingEMI.ClientID %>').data('value'), true);
});
    </script>


<script type="text/javascript">
    function ShowError(ErrorMessages) {

        $("#ErrorPage #lblerror").html(ErrorMessages);
        $("#ErrorPage").modal("show");
    }


</script>

    <script>
        const path = window.location.pathname.toLowerCase();

        if (path.includes("managecustomer")) {
            document.getElementById("toggleCustomer")?.classList.add("active");
        }
        else if (path.includes("manageretailer")) {
            document.getElementById("toggleDealer")?.classList.add("active");
        }
    </script>



    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet">

       <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js'></script>
<script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js'></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">    
</asp:Content>
	    