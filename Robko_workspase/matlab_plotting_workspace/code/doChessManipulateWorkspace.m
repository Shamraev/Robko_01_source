function doChessManipulateWorkspace()
    hold on;
    P = [0,300,190] ;   % you center point 
    L = [220,220,100] ;  % your cube dimensions 
    O = P-L/2 ;       % Get the origin of cube so that P is at center 
    plotcube(L,O,.4,[1 0 0]);   % use function plotcube 
    hold on
    plot3(P(1),P(2),P(3),'*k','MarkerSize',15, 'color', 'blue')       
end

