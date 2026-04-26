/* =========================================
   V3 INVENTORY PIXEL-PERFECT FIGMA DESIGN
   ========================================= */

.v3-container {
    padding: 30px 40px;
    color: #F4EFE6;
    font-family: 'Inter', sans-serif;
    background-color: #162A2C;
    min-height: 100vh;
}

/* Header */
.v3-header-layout {
    display: flex;
    justify-content: space-between;
    align-items: flex-end;
    margin-bottom: 10px;
}

.v3-logo {
    font-size: 64px;
    font-weight: 700;
    margin: 0;
    line-height: 1;
}

.v3-logo-green { color: #24C239; }

.v3-subtitle {
    font-size: 16px;
    color: #FFFFFF;
    margin-top: 5px;
    display: block;
}

.v3-user-area { text-align: right; }
.v3-hello { font-size: 24px; font-weight: 700; margin-bottom: 5px; }
.v3-dot {
    display: inline-block;
    width: 13px; height: 13px;
    background: #33D218;
    border-radius: 50%;
    margin-right: 8px;
}
.v3-date { font-size: 20px; font-weight: 400; }

.v3-divider {
    border: none;
    border-top: 1px solid rgba(255, 255, 255, 0.3);
    margin-bottom: 30px;
}

/* Middle Section */
.v3-middle-section {
    display: flex;
    justify-content: space-between;
    align-items: flex-start;
    margin-bottom: 30px;
    gap: 20px;
}

/* Stat Cards */
.v3-stats-group {
    display: flex;
    gap: 15px;
}

.v3-stat-card {
    background: rgba(0, 0, 0, 0.2);
    border-radius: 23px;
    width: 250px;
    height: 130px;
    display: flex;
    align-items: center;
    padding: 0 25px;
    gap: 20px;
}

.v3-icon-box {
    font-size: 24px;
    width: 48px;
    height: 60px;
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 8px;
}
.v3-border-green { border: 4px solid #1C9A33; color: #1C9A33;}
.v3-border-yellow { border: 3.5px solid #B29F54; color: #B29F54; }
.v3-border-red { border: 3.5px solid #C20A0A; color: #C20A0A; }
.v3-border-blue { border: 3.5px solid #1E88E5; color: #1E88E5; }

.v3-stat-data h2 {
    font-size: 40px;
    font-weight: 700;
    margin: 0;
    line-height: 1;
}

.v3-stat-data p {
    font-size: 16px;
    font-weight: 600;
    margin: 5px 0 0 0;
    line-height: 1.2;
}

.v3-stat-data span {
    font-size: 12px;
    font-weight: 400;
    opacity: 0.8;
}

/* Right Actions */
.v3-action-group {
    display: flex;
    flex-direction: column;
    gap: 12px;
}

.v3-search-bar {
    background: #606C58;
    border-radius: 1000px;
    height: 32px;
    width: 300px;
    display: flex;
    align-items: center;
    padding: 0 15px;
}

.v3-search-bar input {
    background: transparent;
    border: none;
    color: white;
    outline: none;
    width: 100%;
    margin-left: 8px;
    font-size: 15px;
}

.v3-search-bar input::placeholder { color: #E0E0E0; }

.v3-buttons-row {
    display: flex;
    gap: 15px;
}

.v3-action-sq {
    background: rgba(0, 0, 0, 0.2);
    border-radius: 23px;
    width: 90px;
    height: 95px;
    border: none;
    color: white;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    gap: 10px;
}

.v3-action-sq:hover { background: rgba(0, 0, 0, 0.4); }

.v3-btn-icon {
    width: 40px;
    height: 40px;
    border-radius: 8px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 18px;
    font-weight: bold;
}

.v3-action-sq span {
    font-size: 10px;
    font-weight: 600;
}

/* Table */
.v3-table-wrapper {
    background: rgba(0, 0, 0, 0.26);
    border-radius: 23px;
    padding: 20px;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
}

.v3-table {
    width: 100%;
    border-collapse: collapse;
    text-align: left;
}

.v3-table th {
    padding: 15px;
    font-size: 14px;
    font-weight: 700;
    border-bottom: 1px solid rgba(255, 255, 255, 0.2);
}

.v3-table td {
    padding: 15px;
    font-size: 14px;
    font-weight: 400;
}

.v3-status {
    width: 20px;
    height: 20px;
}
.v3-status.green { background: #1C9A33; }
.v3-status.yellow { background: #B29F54; }
.v3-status.red { background: #C20A0A; }