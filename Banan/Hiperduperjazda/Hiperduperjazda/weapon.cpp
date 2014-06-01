#include "weapon.h"

void Wpn::Move(float timeFactor)
{
	for(int i = 0; i < element.size();i++)
	{
		if(SDL_GetTicks() - element.at(i).time > LASER_LIVETIME)
			element.erase(element.begin()+i);
		else
		{
  			element.at(i).x +=speed * timeFactor * element.at(i).direction;
			element.at(i).shape.x = element.at(i).x;
		}
	}
}

void Wpn::Draw(SDL_Surface* dest)
{
	for(int i = 0; i < element.size();i++)
	{
		SDL_FillRect(dest, &element.at(i).shape, GetRandomColor(dest));
	}
}

void Wpn::Shoot(float mx, float my, int direction)
{
	shoot shoot;
	if(element.size() < LASER_MAXQTY)
		{
		if(direction < 0)
		{
			shoot.shape.x = shoot.x = mx - LASER_WIDTH;
			shoot.shape.y = shoot.y = my;
		}
		else if(direction > 0)
		{
			shoot.shape.x = shoot.x = mx;
			shoot.shape.y = shoot.y = my;
		}
		shoot.shape.w = LASER_WIDTH;
		shoot.shape.h = LASER_HEIGHT;
		shoot.direction = direction;
		shoot.exist = true;
		shoot.time = SDL_GetTicks();
		element.push_back(shoot);

	}
}