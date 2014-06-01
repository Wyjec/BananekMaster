#ifndef DEFINES_H_
#define DEFINES_H_

#define SCREEN_WIDTH	800	
#define SCREEN_HEIGHT	600
#define SCREEN_BPP		32

#define FRAMES_PER_SECOND 32

#define GRAVITY 4
#define STATICAL_GRAVITY 30
#define ACCELERATION 2
#define WEAPON_QNTY 8
#define CHAR_WIDTH 25
#define CHAR_HEIGHT 30
#define IDLE_TIME 5000 // idle animation after 5 s
#define VELOCITY_Y 350 // start velocity at jump
#define VELOCITY_X 80 // start velocity at walk
#define WALL_JUMP 150
#define MAX_VELOCITY_X	1000
#define MAX_VELOCITY_Y	2000

#define MAP_MAX_ROWS 8  
#define MAP_MAX_PLATFORMS 5   
#define MIN_SPACE 30					 // minimal space between platforms (a bit bigger than char width)

#define LASER_WIDTH  100
#define LASER_HEIGHT 10
#define LASER_SPEED  10
#define LASER_MAXQTY 30
#define LASER_LIVETIME 5000 // [ms]
typedef long long int LARGE_INTEGER;

#endif