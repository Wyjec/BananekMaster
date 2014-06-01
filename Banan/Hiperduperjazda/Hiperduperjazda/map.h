#ifndef MAP_H_
#define MAP_H_

#include <vector>
#include "SDL.h"
#include <sstream>
#include "defines.h"
#include "engine.h"

extern TTF_Font *font;
extern SDL_Color textColor;

enum MAP_TYPE{
	OPENED,
	CLOSED,
	HORIZONTAL_CLOSED,
	VERTICAL_CLOSED,
	RANDOM
};

class Map
{
private:
	//MAP_TYPE type;
	//unsigned int rows, cols; // row = platform + space beetween floor and ceiling  col = platform + space beetween one and another platform
	int gravity, rows, platforms, color;
	MAP_TYPE type;
	std::vector<SDL_Rect> elements;		
	void GenerateBorders(MAP_TYPE type);
	void GenerateRandomMap(int min_rows, int min_platforms, int max_rows, int max_platforms);

public:
	Map(int grav = GRAVITY) : gravity(grav){}
	int GetMapGravity(){return gravity;}
	std::vector<SDL_Rect> GetElements(){return elements;}
	void AddElement(SDL_Rect element){elements.push_back(element);}
	void GenerateMap(int mapColor, MAP_TYPE mtype = RANDOM, int min_rows = 3, int min_platforms = 1, int max_rows = MAP_MAX_ROWS, int max_platforms = MAP_MAX_PLATFORMS);
	//void GenerateRandomMap(MAP_TYPE type);
	//void GenerateRandomMap(MAP_TYPE type, unsigned int rows);
	//void GenerateRandomMap(MAP_TYPE type, unsigned int rows, unsigned int *cols);
	void Draw(SDL_Surface* surface);
	virtual std::vector<SDL_Surface*> MapInfo();
};
#endif
