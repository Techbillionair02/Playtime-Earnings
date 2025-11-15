# Playtime Earnings  
**Version:** 1.0.0  
**Author:** GaryG / Regime Gaming  

Playtime Earnings is a lightweight Rust plugin that rewards players with in-game currency for every minute they stay online.  
It integrates directly with the **Economics** plugin and pays out automatically at a fixed rate.

---

## ⭐ Features

- Pays players **$20 per hour** ($0.333 per minute)
- Works automatically in the background — no commands required
- Uses a buffer system to prevent rounding errors
- Only pays players with valid SteamIDs
- Cleans up pending balances when players disconnect
- No configuration required — plug-and-play

---

## 💰 Payment Logic

Players earn:

$20 per hour
= $0.333333... per minute

yaml
Copy code

The plugin runs **every 60 seconds**, adds the earned amount to a pending buffer, and deposits whole cents to Economics when available.

This prevents decimal overflow and ensures accurate money handling.

---

## 📦 Requirements

You **must** have the following plugin installed:

- **Economics**  
  https://umod.org/plugins/economics

If Economics is missing, the plugin will automatically disable payouts and warn in console.

---

## ⚙️ Installation

1. Upload `PlaytimeEarnings.cs` to your server’s  
   `oxide/plugins/` folder  
2. Reload manually or allow it to auto-compile  
3. Done — money starts flowing every minute

---

## ❗ Notes

- If the server crashes mid-interval, pending rewards for the last minute may not save (normal behavior).
- This plugin is extremely low-load and safe to run on any Rust server.

---

## ✔️ Future Upgrade Ideas

- VIP multiplier system  
- Different rates per rank / group  
- Offline accumulation toggles  
- Command `/playtime earnings` to check hourly rate  
- Logging / Discord announcements  

---

## 📝 License

Free to use • Free to modify • Regime Gaming Edition 
