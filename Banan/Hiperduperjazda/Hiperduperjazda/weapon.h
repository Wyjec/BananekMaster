#ifndef WEAPON_H_
#define WEAPON_H_

#include "SDL.h"
#include "engine.h"
#include <vector>

struct shoot{
	SDL_Rect shape;
	float x, y;
	int direction;
	int time;
	bool exist;
};

class Wpn{
private:
	std::vector<shoot> element;
	float speed;
	float acc;
public:
	Wpn() : speed(LASER_SPEED), acc(2){};
	void Shoot(float mx, float my, int direction);
	void Move(float timeFactor);
	void Draw(SDL_Surface* dest);
	std::vector<shoot> GetElements(void){return element;}
	void Clear(void){element.clear();}
};
#endif