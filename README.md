# IF3210-2023-Unity-IQI

## Deskripsi Aplikasi
Extended Survival Shooter merupakan game modifiksasi Survival Shooter yang di dalamnya terdapat tambahan story dan beberapa fitur tambahan, seperti main menu, local scoreboard, dan shopkeeper yang menyediakan berbagai jenis weapon dan pet yang dapat dibeli dan digunakan oleh player. Game ini juga menyediakan cheat yang bisa kalian gunakan juga lho!

## Library
- Unity Engine (UnityEngine.SceneManagement, UnityEngine.UI, dll)
- TMPro

## Screenshot

### Opening
![Opening](/game_ss/Opening.jpg?raw=true "Opening")
### Boss
![Boss](/game_ss/Boss.jpg?raw=true "Boss")
### Closing
![Closing](/game_ss/Closing.jpg?raw=true "Closing")
### Quest
![Quest](/game_ss/Quest.jpg?raw=true "Quest")
### Save Game
![Save Game](/game_ss/Save_Game.jpg?raw=true "Save Game")
### Game Over
![Game Over](/game_ss/Game_Over.jpg?raw=true "Game Over")
### Main Menu
![Main Menu](/game_ss/Main_Menu.jpg?raw=true "Main Menu")
### Shopkeeper
![Shopkeeper](/game_ss/Shopkeeper.jpg?raw=true "Shopkeeper")
### Weapon
![Weapon](/game_ss/Weapon.jpg?raw=true "Weapon")
### Pet
![Pet](/game_ss/Pet.jpg?raw=true "Pet")
### Cheat
![Cheat](/game_ss/Cheat.jpg?raw=true "Cheat")

## Features
- Main menu
main menu berisi berbagai pilihan yaitu:
    1. Memulai game baru
    2. Load game yang telah disimpan
    3. Membuka local scoreboard untuk menampilkan riwayat skor hasil permainan yaitu urutan waktu menamatkan game dari 
    4. Membuka menu settings untuk menentukan nama pemain dan mengatuf volume sfx/music
    5. Exit dari game
- Shopkeeper
Shopkeeper hanya dapat di akses setelah quest selesai. Untuk mengakses shopkeeper, tekan `E` pada keyboard. Mengakses shopkeeper hanya dapat dilakukan dari jarak yang cukup dekat, apabila mengakses dari jarak diluar jangkauan akan keluar pesan error. Untuk keluar dari shopkeeper tekan `esc`pada keyboard.
- Weapon
    Terdapat 4 jenis weapon, 1 weapon deafult, 3 weapon tambahan (shotgun, sword, bow) yang dapat dibeli di shopkeeper, cara menggantinya adalah dengan scroll up down menggunakan mouse atau pencet 1(default), 2(shotgun), 3(sword), 4(bow).

- Pet
    Terdapat 3 jenis pet yang dapat dibeli di shopkeeper dengan skill masing-masing sebagai berikut:
    1. Tipe Healer
    Tipe ini akan menambah darah player dalam satuan waktu tertentu Tipe pet ini selalu mengikuti arah gerak player. Pet ini akan bersuara setiap menambahkan darah player. Skill akan hilang setelah pet mati.
    2. Tipe Attacker
    Tipe ini akan membantu player membunuh musuh dari jarak jauh. Sesuai spesifikasi, tipe ini akan mendekati musuh SETELAH menyerang karena menyerang dilakukan dari jarak jauh. Pet ini akan bersuara setiap membunuh musuh. Skill akan hilang setelah pet mati.
    3. Tipe Aura Buff
    Tipe ini akan menambah damage per shot. Tipe pet ini selalu mengikuti arah gerak player. Pet ini akan bersuara hanya sekali yaitu pada setiap awal game. Skill akan hilang setelah pet mati.
- Cheat
    Cara mengakses cheat adalah menggunakan backtick. Terdapat beberapa kata yang dapat diinput untuk cheat yaitu:
    - nodamage: HP dari player tidak akan berkurang jika diserang oleh mob.
    - onehitkill: 1 serangan dari player akan langsung membunuh mob yang diserang.
    - motherload: Player akan mendapatkan uang yang tak terhingga.
    - doublespeed: Kecepatan pergerakan dari player bertambah sebanyak 2 kali lipat.
    - fullhppet: HP dari pet tidak akan berkurang jika diserang oleh mob.
    - killpet: Membunuh pet secara instant.


## Pembagian tugas
| Nama                   |     NIM    | Jumlah Jam |           Tasks         |
|------------------------|:----------:|:----------:|:-----------------------:|
| Tri Sulton Adila       |  13520033  | 24          |Weapon shotgun|
| Fikri Khoiron Fadhila  |  13520056  | 36          |Shopkeeper, weapon sword, cheat|
| Rifqi Naufal Abdjul    |  13520062  | 36          |story mode, save game, game over, local scoreboard, main menu, all manager|
| Ziyad Dhia Rafi        |  13520064  | 36          |Pet attacker, boss scene, weapon bow|
| Grace Claudia          |  13520078  | 36          |Basegame. pet healer, pet aura buff, cutscene 1, cutscene 2, cutscene 3|