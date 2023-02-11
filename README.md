# AirplanSeatReservator

This project accomodates a specified number of passengers into an airplan seats considering following rules:


1- Always seat passengers star:ng from the front row to back,  star:ng from the le= to the right  
2- Fill aisle seats first followed by window seats followed by center  seats (any order in center seats)  

Input to the program will be   
 a 2D array that represents the rows and columns [ [3,4], [4,5],  [2,3], [3,4] ]  
• Number of passengers waiting in queue. 

Sample reserved seats for a 2D array like: [ [3,2], [4,3], [2,3], [3,4] ]

<img src="https://github.com/hmirheydari/AirplanSeatReservator/blob/main/airplane-seats.jpg" alt="Airplan reserved seats">


## Development Environment

Visual Studio 2022 with ".Net development workload" installed

.Net framework 4.5.2

## Run Locally

Clone the project

```bash
  git clone https://github.com/hmirheydari/AirplanSeatReservator.git
```

Go to the project directory

```bash
  cd AirplanSeatReservator
```

Build the project

```bash
  msbuild
```

Run the project

```bash
  cd AirplanSeatReservator\bin\debug
  AirplanSeatReservator.exe
```
