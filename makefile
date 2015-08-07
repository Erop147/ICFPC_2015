XBUILD := xbuild

XBUILDFLAGS := /p:TargetFrameworkProfile=''
XBUILDFLAGS_RELEASE := $(XBUILDFLAGS) /p:Configuration=Release

SOLUTION := Game/Game.sln

deploy: createrunner
	chmod +rx play_icfp2015

createrunner: clearrunner
	cp -f play_icfp2015.base play_icfp2015

clearrunner: release
	rm -f play_icfp2015

release:
	$(XBUILD) $(XBUILDFLAGS_RELEASE) $(SOLUTION)

clean: clearrunner
	$(XBUILD) $(XBUILDFLAGS_RELEASE) $(SOLUTION) /t:Clean

.SUFFIXES:
.PHONY: deploy release  clean