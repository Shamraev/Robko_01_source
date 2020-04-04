%This function set Robko 01 Workspace Points%
function [x,y,z] = getWorkspacePoints(n)
    %n - how many points, 15 - normal
    d2=190;d3=178;d4=177;d5=80; %lengths of robot links
        
    q1=linspace(-pi/3,pi/3,n); % n 1d points between [-pi/3,pi/3]
    q2=linspace(-pi/6,pi/2,n);
    q3=linspace(0,(3*pi)/4,n);

    [Q1,Q2,Q3]=ndgrid(q1,q2,q3);  % This will create matrices of nxnxn for each variable

    %ограничение на минимальный угол между звеньями d3 и d4: 35 градусов = 0.6 рад
    for i = 1:n
        for j = 1:n
            for k = 1:n
                if (pi/2 + Q2(i, j, k) - Q3(i, j, k)) <= 0.6 %delete unsatisfied Q2, Q3
                    Q2(i, j, k) = 0;
                    Q3(i, j, k) = 0;
                end
            end
        end
    end

    rM = round(sin(Q2).*d3 + cos(Q3).*d4 + d5);
    xM = round(sin(Q1).*rM);
    yM = round(cos(Q1).*rM);
    zM = round(cos(Q2).*d3 - sin(Q3).*d4 + d2);

    x = xM(:); %(:) converts any matrix into a list of its elements in one single column.
    y = yM(:); 
    z = zM(:);  
end

