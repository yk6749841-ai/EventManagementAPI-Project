# Event Management API

מערכת לניהול אירועים וכנסים, המאפשרת לארגן ולתעד אירועים בצורה נוחה ומסודרת.  
באמצעות המערכת ניתן להוסיף אירועים חדשים, לנהל את משתתפי האירועים, לבדוק את זמינות המקומות, ולעקוב אחר הרשמות וסטטוס ההשתתפות של כל משתתף.  
המערכת מספקת ממשק ברור לצפייה, סינון ועדכון של האירועים והמשתתפים, ומאפשרת ניהול קל ואפקטיבי של אירועים מכל סוג.

---

## ישויות עיקריות

| ישות | תכונות עיקריות |
|------|----------------|
| **אירוע (Event)** | id, title, date_time, location, capacity, description |
| **משתתף (Participant)** | id, name, email, phone |
| **הרשמה (Registration)** | id, event_id, participant_id, status, registration_date |

---

## Endpoints

### 1. אירוע (Event)

| פעולה | HTTP Method | URL |
|-------|------------|-----|
| שליפת רשימת האירועים | GET | `/events` |
| שליפת אירוע לפי מזהה | GET | `/events/{id}` |
| הוספת אירוע | POST | `/events` |
| עדכון אירוע | PUT | `/events/{id}` |
| מחיקת אירוע | DELETE | `/events/{id}` |
| בדיקת מספר המקומות הפנויים | GET | `/events/{id}/availability` |

---

### 2. משתתף (Participant)

| פעולה | HTTP Method | URL |
|-------|------------|-----|
| שליפת רשימת המשתתפים | GET | `/participants` |
| שליפת משתתף לפי מזהה | GET | `/participants/{id}` |
| הוספת משתתף | POST | `/participants` |
| עדכון משתתף | PUT | `/participants/{id}` |
| מחיקת משתתף | DELETE | `/participants/{id}` |
| שליפת כל ההרשמות של משתתף מסוים | GET | `/participants/{id}/registrations` |

---

### 3. הרשמה (Registration)

| פעולה | HTTP Method | URL |
|-------|------------|-----|
| שליפת רשימת ההרשמות | GET | `/registrations` |
| שליפת הרשמה לפי מזהה | GET | `/registrations/{id}` |
| הוספת הרשמה | POST | `/registrations` |
| עדכון הרשמה | PUT | `/registrations/{id}` |
| מחיקת הרשמה | DELETE | `/registrations/{id}` |
| שינוי סטטוס הרשמה | PUT | `/registrations/{id}/status` |
