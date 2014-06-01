#ifndef CHARACTER_H_
#define CHARACTER_H_

#include <string>
#include <sstream>
#include <cstdio>
#include <iostream>
#include <vector>
#include "SDL.h"
#include "engine.h"
#include "map.h"
#include "weapon.h"

extern bool hud;
extern bool quit;
extern SDL_Event event;
extern TTF_Font *font;
extern SDL_Color textColor;
extern Map mapa;
extern Wpn bron;
enum ANIMATION {
	STOP,
	IDLE,
	WALK,
	JUMP,
	FALL,
	CROUCH,
	DEAD
};

enum TURN {
	LEFT,
	RIGHT,
	OTHER
};

enum ENVIRO {
	GROUND,
	AIR,
	WALL
};

struct Sprite {
	float x;
	float y;
	int w;
	int h;
	int offsetX;
	int offsetY;
	int frame;
	SDL_Surface* surface;
};

struct Velocity {
	float x;
	float y;
	float acc;
	float gravity;
};

struct Weapon{
	bool enabled;
	int ammo;
};

struct State {
	ANIMATION animation;
	TURN turn;
	ENVIRO enviro;
	bool djump;
};

class Character
{
protected:
	Velocity velocity;
	Weapon weapons[WEAPON_QNTY];
	Sprite sprite;
	State state;
	int health;	

public:
	Character(int x, int y);
	~Character();
	void handle_input();
	float GetX(){return sprite.x;}
	float GetY(){return sprite.y;}
	virtual void RandomizePosition();
	virtual void Dynamic();
	virtual void Move(float timeFactor, std::vector<SDL_Rect> collElements);
	virtual void Show(SDL_Surface* destination);
	virtual bool checkCollision(std::vector<SDL_Rect> collElements);
	virtual bool checkCollision(std::vector<shoot> collElements);
	virtual bool LoadFiles() = 0;
	virtual std::vector<SDL_Surface*> PlayerInfo();
};

class Banana : public Character
{
private:
	int BananaRage;

public:
	Banana(int x, int y);
	bool LoadFiles();
	//void Show();
};

#endif